namespace BGLibExt
{
    internal interface IByteSerializable
    {
        void DeSerialize(byte[] bytes);
        byte[] Serialize();
    }
}
