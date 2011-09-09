using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace shooter02.Managers
{
    class TextureManager
    {
        // Singleton setup
        private static readonly TextureManager instance = new TextureManager();
        public static TextureManager Instance
        {
            get
            {
                return instance;
            }
        }

        // data members
        private List<Texture2D> m_vTextures;

        // constructor
        public TextureManager()
        {
            m_vTextures = new List<Texture2D>();
        }

        // methods
        /// <summary>
        /// Loads the specified texture into the DB.
        /// </summary>
        /// <param name="szFilename">Relative file path to the texture.</param>
        /// <returns></returns>
        public int LoadTexture(string szAssetname)
        {
            // If texture already exists in the DB, give the ID
            int result = TextureExists(szAssetname);
            if (result >= 0)
                return result;

            // If not already in the DB, add it
            Texture2D newTexture = StateManager.Instance.ContentManagerInstance.Load<Texture2D>(szAssetname);
            m_vTextures.Add(newTexture);

            return m_vTextures.Count - 1; // Give the ID back
        }

        /// <summary>
        /// Renders the specified texture on screen
        /// </summary>
        /// <param name="nTexture">ID of the texture to be drawn</param>
        /// <param name="vPosition">The location (in screen coordinates) to draw the texture.</param>
        /// <param name="rSourceRect">A rectangle that specifies (in texels) the source texels from a texture. Use null to draw the entire texture.</param>
        /// <param name="cColor">The color used to tint the texture</param>
        /// <param name="vOrigin">The sprite's origin. default is (0,0) which is the upper-left corner</param>
        /// <param name="vScale">scale factor for the texture</param>
        /// <param name="effect">Effects to apply to the texture</param>
        /// <param name="fLayerDepth">The depth of a layer in which the texture will be drawn</param>
        public void DrawTexture(int nTexture, Vector2 vPosition, Nullable<Rectangle> rSourceRect, Color cColor, Vector2 vOrigin, Vector2 vScale, SpriteEffects effect, float fLayerDepth)
        {
            // TODO: Render stuff
        }

        // helper functions
        private int TextureExists(string szAssetname)
        {
            for (int i = 0; i < m_vTextures.Count; i++)
                if (m_vTextures[i].Name == szAssetname)
                    return i; // texture found, return its ID

            return -1; // texture was not found
        }

        private bool ValidIndex(int nIndex)
        {
            if (nIndex < 0 || nIndex > m_vTextures.Count - 1)
                return false;
            else
                return true;
        }
    }
}
