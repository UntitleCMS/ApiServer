namespace Api.Utills.SaveFile
{
    public interface ISaveFileStrategy<DataType>
    {
        public void Save(in DataType data, out string fileName, string extension = "");
    }
}
