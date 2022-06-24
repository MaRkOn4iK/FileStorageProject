using DAL.Entities;

namespace FileStorageTests.Helpers
{
    internal class FakeFullFileInfoDbSet : FakeDbSet<FullFileInfo>
    {
        public override Task<FullFileInfo> FindAsync(params object[] keyValues)
        {
            int tmp = (int)keyValues[0];
            return Task.FromResult(this._data.Where(res => res.Id == tmp).FirstOrDefault());
        }

    }
}
