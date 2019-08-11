using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PlanYourDate.Model;

namespace PlanYourDate.DAL
{
    public class PlaceRepository : IPlaceRepository, IDisposable
    {
        private PlacesForDateContext context;

        public PlaceRepository(PlacesForDateContext context)
        {
            this.context = context;
        }

        public IEnumerable<Places> GetPlace()
        {
            return context.Places.ToList();
        }

        public Places GetPlaceByID(int PlaceId)
        {
            return context.Places.Find(PlaceId);
        }

        public void InsertPlace(Places place)
        {
            context.Places.Add(place);
        }

        public void DeletePlace(int PlaceId)
        {
            Places place = context.Places.Find(PlaceId);
            context.Places.Remove(place);
        }

        public void UpdatePlace(Places place)
        {
            context.Entry(place).State = EntityState.Modified;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
