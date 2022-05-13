using System;
using ExtraAssets.Scripts.Enemy;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Map.Section;

namespace ExtraAssets.Scripts.Map.Provider
{
    public class SectionGenerator
    {
        private const int GenerationCounter = 4;

        private readonly SectionControl _sectionControl;

        private readonly TrackSectionFactory _trackSectionFactory;
        private readonly WallFactory _wallFactory;
        private readonly CubePackFactory _cubePackFactory;
        private readonly CubeObjectFactory _cubeObjectFactory;

        private Action<TrackSection> _onSectionGenerated;

        public SectionGenerator(
            SectionControl sectionControl,
            TrackSectionFactory trackSectionFactory, 
            WallFactory wallFactory, 
            CubePackFactory cubePackFactory,
            CubeObjectFactory cubeObjectFactory, 
            Action<TrackSection> onSectionGenerated)
        {
            _sectionControl = sectionControl;

            _trackSectionFactory = trackSectionFactory;
            _wallFactory = wallFactory;

            _onSectionGenerated = onSectionGenerated;
            _cubePackFactory = cubePackFactory;
            _cubeObjectFactory = cubeObjectFactory;
        }

        public void GenerateAllTrack()
        {
            GenerateFirstSection();
            
            for (int i = 0; i < GenerationCounter; i++) 
                GenerateNextSection();
        }

        public void GenerateNextSection()
        {
            TrackSection section = CreateSection();
            
            EnemyWall wall = CreateWall();
            section.AttachWall(wall);

            CubePack cubePack = CreateCubePack();
            section.AttachCubePack(cubePack);
            
            _sectionControl.Attach(section);
            
            _onSectionGenerated?.Invoke(section);
        }

        private CubePack CreateCubePack() => 
            _cubePackFactory.Take();

        private void GenerateFirstSection()
        {
            TrackSection section = CreateSection();
            _sectionControl.Attach(section);
            
            _onSectionGenerated?.Invoke(section);
        }

        private TrackSection CreateSection() => 
            _trackSectionFactory.Take();

        private EnemyWall CreateWall() => 
            _wallFactory.Take();
    }
}