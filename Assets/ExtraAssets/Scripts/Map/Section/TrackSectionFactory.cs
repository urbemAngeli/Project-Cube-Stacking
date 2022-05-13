using ExtraAssets.Scripts.Infrastructure;
using ExtraAssets.Scripts.Services.AssetManagement;
using UnityEngine;

namespace ExtraAssets.Scripts.Map.Section
{
    public class TrackSectionFactory
    {
        private PoolMono<TrackSection> _sections;

        public TrackSectionFactory(IAssetProvider assetProvider)
        {
            TrackSection sectionPrefab = assetProvider.Load<TrackSection>(AssetPath.TRACK_SECTION_PATH);
            
            Transform root = new GameObject("[TrackSectionFactory]").transform;
            _sections = new PoolMono<TrackSection>(sectionPrefab, 5, root, false);
        }

        public TrackSection Take()
        {
            TrackSection section = _sections.Take();
            section.Initialize();
            return section;
        }

        public void Put(TrackSection section)
        {
            section.Dispose();
            _sections.Put(section);
        }
    }
}