using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace TDC2015.BackgroundTask
{
    public sealed class TileUpdaterTask : IBackgroundTask
    {
        private static string w10TileXml = @"<tile>
                                <visual>
                                 <binding template=""TileSmall"">
                                  <text>TDC</text>
                                  <text>UW</text>
                                 </binding>
                                 <binding template=""TileMedium"">
                                  <image placement=""peek"" src=""Assets\Tile150x150.png"" />
                                  <image src=""Assets\Tile_w10_150x150.png"" />                                  
                                 </binding>
                                 <binding template=""TileWide"">                                  
                                  <group>
                                   <subgroup hint-weight=""33"">
                                    <image placement=""inline"" src=""Assets\TDC_w10_310X150.png"" />
                                   </subgroup>
                                   <subgroup>
                                    <text hint-style=""caption"" hint-wrap=""true"" hint-maxLines=""3"">
                                      Trilha Universal Windows
                                    </text>
                                    <text hint-style=""captionsubtle"" hint-wrap=""true"" hint-maxLines=""3"">
                                      Background Tasks
                                    </text>
                                   </subgroup>
                                  </group>
                                 </binding>
                                 <binding template=""TileLarge"" hint-textStacking=""center"" branding=""name"">
                                  <group>
                                   <subgroup hint-weight=""1"" />
                                   <subgroup hint-weight=""2"">
                                    <image src=""Assets\Tile_w10_150x150.png"" hint-crop=""circle"" />
                                   </subgroup>
                                   <subgroup hint-weight=""1"" />
                                  </group>
                                  <text hint-style=""title"" hint-wrap=""true"" hint-maxLines=""3"" hint-align=""center"">
                                    Universal Windows
                                  </text>
                                  <text hint-style=""caption"" hint-wrap=""true"" hint-maxLines=""3"" hint-align=""center"">
                                    Background Task
                                  </text>
                                 </binding>
                                </visual>
                               </tile>";

        public void Run(IBackgroundTaskInstance taskInstance)
        {
            var cost = BackgroundWorkCost.CurrentBackgroundWorkCost;
            if (cost == BackgroundWorkCostValue.High)
            {
                return;
            }

            var cancel = new System.Threading.CancellationTokenSource();
            taskInstance.Canceled += (s, e) =>
            {
                cancel.Cancel();
                cancel.Dispose();
            };

            var deferral = taskInstance.GetDeferral();
            try
            {
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.LoadXml(TileUpdaterTask.w10TileXml);

                TileNotification tileNotification = new TileNotification(xmldoc);
                TileUpdater tileUpdator = TileUpdateManager.CreateTileUpdaterForApplication();
                tileUpdator.Update(tileNotification);
            }
            finally
            {
                deferral.Complete();
            }
        }
    }
}
