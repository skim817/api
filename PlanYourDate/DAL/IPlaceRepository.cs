using System;
using System.Collections.Generic;
using PlanYourDate.Model;

namespace PlanYourDate.DAL
{
    public interface IPlaceRepository : IDisposable
    {
        IEnumerable<Places> GetPlace();
        Places GetPlaceByID(int PlaceId);
        void InsertPlace(Places place);
        void DeletePlace(int PlaceId);
        void UpdatePlace(Places place);
        void Save();
    }
}
