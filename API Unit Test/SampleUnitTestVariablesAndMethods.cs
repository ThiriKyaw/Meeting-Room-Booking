using DataLayer.DBModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace API_Unit_Test
{
    public class SampleUnitTestVariablesAndMethods
    {
        public static class GenericInfoTest
        {
            public static int versionNo = 1;
            public static bool isDeleted = false;
            public static string createdById = "System";
            public static string createdBy = "System";
            public static DateTime? createdDate = DateTime.Now;
            public static string lastUpdatedById = "System";
            public static string lastUpdatedBy = "System";
            public static DateTime? lastUpdatedDate = DateTime.Now;

            public static string delimiter = "|Delimiter|";
        }

        public static class MemoryDatabase
        {
            public static void CreateSampleData(dynamic obj, MeetingRoomBookingContext _contextMemory)
            {
                if (obj != null)
                {
                    Type listType = typeof(List<>);
                    Type type = obj?.GetType();
                    if (type.HasSameMetadataDefinitionAs(listType))
                    {
                        _contextMemory.AddRange(obj);

                    }
                    else
                    {
                        _contextMemory.Add(obj);
                    }
                }
            }

            public static void DetachSampleData(dynamic obj, MeetingRoomBookingContext _contextMemory)
            {
                if (obj != null)
                {
                    Type listType = typeof(List<>);
                    Type type = obj?.GetType();
                    if (type.HasSameMetadataDefinitionAs(listType))
                    {
                        foreach (var dyn in obj)
                        {
                            _contextMemory.Entry(dyn).State = EntityState.Detached;
                        }
                    }
                    else
                    {
                        _contextMemory.Entry(obj).State = EntityState.Detached;
                    }
                }
            }

        

            public static void RemoveAllSampleData(MeetingRoomBookingContext _contextMemory)
            {
                _contextMemory.Database.EnsureDeleted();
                _contextMemory.SaveChanges();
            }
        }

        public static class Environments
        {
            public static void removeAllEnvironmentVariables()
            {
               
            }
        }

    }
}
