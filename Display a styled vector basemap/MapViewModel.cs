using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Esri.ArcGISRuntime.Data;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Location;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Security;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.Tasks;
using Esri.ArcGISRuntime.UI;
using Esri.ArcGISRuntime.Portal;

namespace Display_a_styled_vector_basemap
{
    /// <summary>
    /// Provides map data to an application
    /// </summary>
    public class MapViewModel : INotifyPropertyChanged
    {
        public MapViewModel()
        {
            CreateStyledVectorBasemap();
        }

        private Map _map;

        private async void CreateStyledVectorBasemap()
        {
            string customVectorLayerId = "d2ff12395aeb45998c1b154e25d680e5";
          
            ArcGISPortal arcGISOnline = await ArcGISPortal.CreateAsync(new Uri("https://www.arcgis.com"));

            PortalItem layerPortalItem = await PortalItem.CreateAsync(arcGISOnline, customVectorLayerId);
            ArcGISVectorTiledLayer customVectorTileLayer = new ArcGISVectorTiledLayer(layerPortalItem);

            Basemap vectorLayerBasemap = new Basemap(customVectorTileLayer);
            Map map = new Map(vectorLayerBasemap);

            double initialLatitude = 34.09042;
            double initialLongitude = -118.71511;
            double initialScale = 300000;
            map.InitialViewpoint = new Viewpoint(initialLatitude, initialLongitude, initialScale);

            Map = map;
        }

        /// <summary>
        /// Gets or sets the map
        /// </summary>
        public Map Map
        {
            get { return _map; }
            set { _map = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// Raises the <see cref="MapViewModel.PropertyChanged" /> event
        /// </summary>
        /// <param name="propertyName">The name of the property that has changed</param>
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var propertyChangedHandler = PropertyChanged;
            if (propertyChangedHandler != null)
                propertyChangedHandler(this, new PropertyChangedEventArgs(propertyName));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
