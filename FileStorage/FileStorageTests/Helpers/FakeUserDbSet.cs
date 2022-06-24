using DAL.Entities;

namespace FileStorageTests.Helpers
{
    internal class FakeUserDbSet : FakeDbSet<User>
    {
        public override Task<User> FindAsync(params object[] keyValues)
        {
            int tmp = (int)keyValues[0];
            return Task.FromResult(this._data.Where(res => res.Id == tmp).FirstOrDefault());
        }
    }
}
