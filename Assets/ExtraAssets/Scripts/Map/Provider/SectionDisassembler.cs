using System.Collections.Generic;
using ExtraAssets.Scripts.Enemy;
using ExtraAssets.Scripts.Map.Cube;
using ExtraAssets.Scripts.Map.Section;

namespace ExtraAssets.Scripts.Map.Provider
{
    public class SectionDisassembler
    {
        private readonly TrackSectionFactory _trackSectionFactory;
        private readonly WallFactory _wallFactory;
        private readonly CubePackFactory _cubePackFactory;
        private readonly CubeObjectFactory _cubeObjectFactory;

        public SectionDisassembler(
            TrackSectionFactory trackSectionFactory,
            WallFactory wallFactory,
            CubePackFactory cubePackFactory,
            CubeObjectFactory cubeObjectFactory)
        {
            _trackSectionFactory = trackSectionFactory;
            _wallFactory = wallFactory;
            _cubePackFactory = cubePackFactory;
            _cubeObjectFactory = cubeObjectFactory;
        }

        public void Disassemble(List<TrackSection> sections)
        {
            for (int i = 0; i < sections.Count; i++)
            {
                Disassemble(sections[i]);
                sections.RemoveAt(i);
                
                i--;
            }
        }
        
        public void Disassemble(TrackSection section)
        {
            DetachEnemyWall(section);
            DetachCubePack(section);

            _trackSectionFactory.Put(section);
        }

        private void DetachCubePack(TrackSection section)
        {
            if (section.TryDetachCubePack(out CubePack cubePack))
            {
                DisassembleCubePack(cubePack);

                _cubePackFactory.Put(cubePack);
            }
        }

        private void DetachEnemyWall(TrackSection section)
        {
            if (section.TryDetachWall(out EnemyWall enemyWall))
                _wallFactory.Put(enemyWall);
        }

        private void DisassembleCubePack(CubePack cubePack)
        {
            CubeObject cubeObject;
            
            for (int i = 0; i < cubePack.Length; i++)
            {
                if (cubePack[i].IsContainsCube)
                {
                    cubeObject = cubePack[i].RemoveCube();
                    _cubeObjectFactory.Put(cubeObject);
                }
            }
        }
    }
}