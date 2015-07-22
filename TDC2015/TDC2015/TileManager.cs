using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace TDC2015
{
    /// <summary>
    ///  Tile Manager class
    /// </summary>
    public class TileManager
    {
        /// <summary>
        /// Method responsible for creating and update adaptive tile
        /// </summary>
        public static void CreateTile()
        { 
            // Simple tile xml. Only text element in all tile templates
            //string tileXml = @"<tile>
            //                    <visual>
            //                     <binding template=""TileSmall"">
            //                      <text>Small</text>
            //                     </binding>
            //                     <binding template=""TileMedium"">
            //                      <text>Medium</text>
            //                     </binding>
            //                     <binding template=""TileWide"">
            //                      <text>Wide</text>
            //                     </binding>
            //                     <binding template=""TileLarge"">
            //                      <text>Large</text>
            //                     </binding>
            //                    </visual>
            //                   </tile>";

            // Complete tile XML.
            string tileXml = @"<tile>
                                <visual>
                                 <binding template=""TileSmall"">
                                  <text>Small</text>
                                 </binding>
                                 <binding template=""TileMedium"" branding=""name"">
                                  <image placement=""peek"" src=""Assets\Tile310x150.png"" />
                                  <text hint-wrap=""true"" hint-maxLines=""3"">Trilha Universal Windows</text>
                                 </binding>
                                 <binding template=""TileWide"">                                  
                                  <group>
                                   <subgroup hint-weight=""33"">
                                    <image placement=""inline"" src=""Assets\TDCLogo.png"" />
                                   </subgroup>
                                   <subgroup>
                                    <text hint-style=""caption"" hint-wrap=""true"" hint-maxLines=""3"">
                                      Trilha Universal Windows
                                    </text>
                                    <text hint-style=""captionsubtle"" hint-wrap=""true"" hint-maxLines=""3"">
                                      Criando Adaptive Tiles
                                    </text>
                                   </subgroup>
                                  </group>
                                 </binding>
                                 <binding template=""TileLarge"" hint-textStacking=""center"" branding=""name"">
                                  <group>
                                   <subgroup hint-weight=""1"" />
                                   <subgroup hint-weight=""2"">
                                    <image src=""Assets\Tile150x150.png"" hint-crop=""circle"" />
                                   </subgroup>
                                   <subgroup hint-weight=""1"" />
                                  </group>
                                  <text hint-style=""title"" hint-wrap=""true"" hint-maxLines=""3"" hint-align=""center"">
                                    Universal Windows
                                  </text>
                                  <text hint-style=""caption"" hint-wrap=""true"" hint-maxLines=""3"" hint-align=""center"">
                                    Criando Live Tiles
                                  </text>
                                 </binding>
                                </visual>
                               </tile>";

            // Create a XMLDocument with tile xml content
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.LoadXml(tileXml);

            // Create a TileNotification with xml document
            TileNotification tileNotification = new TileNotification(xmldoc);
            TileUpdater tileUpdator = TileUpdateManager.CreateTileUpdaterForApplication();            
            // Update tile with the new tile layout
            tileUpdator.Update(tileNotification);
        }            
    }
}
