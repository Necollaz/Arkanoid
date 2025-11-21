using System.Collections.Generic;

namespace MiniIT.ARKANOID
{
    public struct BrickSet
    {
        public BrickConfig             Config;
        public List<BrickCellPosition> Positions;

        public BrickSet(BrickConfig config, List<BrickCellPosition> positions)
        {
            Config = config;
            Positions = positions;
        }
    }
}