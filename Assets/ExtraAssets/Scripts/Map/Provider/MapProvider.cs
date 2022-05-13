using System.Collections.Generic;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Map.Section;

namespace ExtraAssets.Scripts.Map.Provider
{
    public class MapProvider : IMapProvider
    {
        private SectionGenerator _generator;
        private SectionDisassembler _disassembler;

        private readonly SectionControl _sectionControl;
        private readonly TrackSectionFactory _trackSectionFactory;
        private readonly WallFactory _wallFactory;
        private readonly CubePackFactory _cubePackFactory;
        private readonly CubeObjectFactory _cubeObjectFactory;

        public MapProvider(
            SectionControl sectionControl,
            TrackSectionFactory trackSectionFactory,
            WallFactory wallFactory,
            CubePackFactory cubePackFactory, 
            CubeObjectFactory cubeObjectFactory)
        {
            _sectionControl = sectionControl;
            _trackSectionFactory = trackSectionFactory;
            _wallFactory = wallFactory;
            _cubePackFactory = cubePackFactory;
            _cubeObjectFactory = cubeObjectFactory;

            _generator = new SectionGenerator(
                _sectionControl, 
                _trackSectionFactory,
                _wallFactory, 
                cubePackFactory,
                cubeObjectFactory,
                OnSectionGenerated);
            
            _disassembler = new SectionDisassembler(_trackSectionFactory, _wallFactory, _cubePackFactory, _cubeObjectFactory);
        }

        public void Create() => 
            _generator.GenerateAllTrack();

        public void Destruct()
        {
            List<TrackSection> sections = _sectionControl.DetachAll();
            _disassembler.Disassemble(sections);

            _cubeObjectFactory.PutAll();
        }

        private void OnSectionGenerated(TrackSection section) => 
            section.OnGrilled += DisassembleSection;

        private void DisassembleSection(TrackSection section)
        {
            _disassembler.Disassemble(section);
            _generator.GenerateNextSection();
        }
    }
}