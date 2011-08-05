using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Xna.Framework;

namespace shooter02.Threading
{
    class DoubleBuffer
    {
        private ChangeBuffer[] m_arrbuffers;
        private volatile int m_nCurrentUpdateBuffer;
        private volatile int m_nCurrentRenderBuffer;
        private AutoResetEvent m_RenderFrameStart;
        private AutoResetEvent m_RenderFrameEnd;
        private AutoResetEvent m_UpdateFrameStart;
        private AutoResetEvent m_UpdateFrameEnd;
        private volatile GameTime m_Gametime;

        public DoubleBuffer()
        {
            // allocate buffers
            m_arrbuffers = new ChangeBuffer[2];
            m_arrbuffers[0] = new ChangeBuffer();
            m_arrbuffers[1] = new ChangeBuffer();

            //Create WaitHandlers
            m_RenderFrameStart = new AutoResetEvent(false);
            m_RenderFrameEnd = new AutoResetEvent(false);
            m_UpdateFrameStart = new AutoResetEvent(false);
            m_UpdateFrameEnd = new AutoResetEvent(false);

            // reset the values
            Reset();

        }

        public void Reset()
        {
            // reset current update/render buffer
            m_nCurrentUpdateBuffer = 0;
            m_nCurrentRenderBuffer = 1;

            // set all events to not signaled
            m_RenderFrameStart.Reset();
            m_RenderFrameEnd.Reset();
            m_UpdateFrameStart.Reset();
            m_UpdateFrameEnd.Reset();
        }

        public void CleanUp()
        {
            // release resources
            m_RenderFrameStart.Close();
            m_RenderFrameEnd.Close();
            m_UpdateFrameStart.Close();
            m_UpdateFrameEnd.Close();
        }

        private void SwapBuffers()
        {
            m_nCurrentRenderBuffer = m_nCurrentUpdateBuffer;
            m_nCurrentUpdateBuffer = (m_nCurrentUpdateBuffer + 1) % 2;
        }

        public void GlobalStartFrame(GameTime gameTime)
        {
            // store gameTime so that Update/Render threads have access
            m_Gametime = gameTime;

            SwapBuffers();

            // give the Update/Render the green light
            m_RenderFrameStart.Set();
            m_UpdateFrameStart.Set();
        }

        public void GlobalSync()
        {
            // wait until both threads are finished with process
            m_RenderFrameEnd.WaitOne();
            m_UpdateFrameEnd.WaitOne();
        }

        public void StartUpdateProcessing(out ChangeBuffer updateBuffer, out GameTime gameTime)
        {
            // wait for the green light
            m_UpdateFrameStart.WaitOne();

            // get the current update buffer
            updateBuffer = m_arrbuffers[m_nCurrentUpdateBuffer];

            // get the time
            gameTime = m_Gametime;
        }

        public void StartRenderProcessing(out ChangeBuffer renderBuffer, out GameTime gameTime)
        {
            // wait for the green light
            m_RenderFrameStart.WaitOne();

            // get the current render Buffer
            renderBuffer = m_arrbuffers[m_nCurrentRenderBuffer];

            // get the time
            gameTime = m_Gametime;
        }

        public void SubmitUpdate()
        {
            // signal that updating is done
            m_UpdateFrameEnd.Set();
        }

        public void SubmitRender()
        {
            // signal that rendering is done
            m_RenderFrameEnd.Set();
        }
    }
}
