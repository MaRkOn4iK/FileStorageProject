namespace FileStorageTests.Helpers
{
    internal class FakeFileDbSet : FakeDbSet<DAL.Entities.File>
    {
        public override Task<DAL.Entities.File> FindAsync(params object[] keyValues)
        {
            int tmp = (int)keyValues[0];
            return Task.FromResult(this._data.Where(res => res.Id == tmp).FirstOrDefault());
        }
    }
}
