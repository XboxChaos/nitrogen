using Nitrogen.ContentData.GameVariants.Megalo;
using Nitrogen.IO;

namespace Nitrogen.Games.Halo4.ContentData.GameVariants.Megalo.ParameterTypes
{
    public class StringToken
        : ISerializable<BitStream>
    {
        public void Serialize(BitStream s)
        {
            /* int type = -1;
            if (Mode == SerializationMode.Deserialize)
            {
                type = Convert.ToInt32(Read(typeof(byte), n: 3)) - 1;
                SetValue("Type", type);
            }
            else
            {
                type = GetValue<int>("Type");
                Write(type + 1, n: 3);
            }

            switch (type)
            {
                case 0:
                    Import<PlayerReferenceData>();
                    break;

                case 1:
                    Import<TeamReferenceData>();
                    break;

                case 2:
                    Import<ObjectReferenceData>();
                    break;

                case 3:
                case 4:
                    Import<IntegerReferenceData>();
                    break;

                case 5:
                    Import<TimerReferenceData>();
                    break;
            }*/
        }
    }
}
