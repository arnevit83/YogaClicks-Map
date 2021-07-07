using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using Clicks.Yoga.Context;
using Clicks.Yoga.Domain.Entities;
using Clicks.Yoga.Geography;

namespace Clicks.Yoga.Data.Search
{
    public class SearchEngine : ISearchEngine
    {
        public SearchEngine(YogaDataContext context)
        {
            Context = context;
        }

        private DbContext Context { get; set; }

        public SearchResponse Search(SearchCriteria criteria)
        {
            string distanceExpression = null;
            string boundsWkt = null;

            if (criteria.Coordinates != null)
            {
                distanceExpression = string.Format(
                    Sql.DistanceExpression,
                    criteria.Coordinates.Longitude,
                    criteria.Coordinates.Latitude,
                    GeographicPoint.CoordinateSystem
                );
            }

            var conditions = new List<string>();

            if (criteria.EntityTypeIds.Count > 0)
            {
                var list = string.Join(", ", criteria.EntityTypeIds.Select(id => id.ToString(CultureInfo.InvariantCulture)));
                conditions.Add(string.Format(Sql.EntityTypeFilter, list));
            }

            if (!string.IsNullOrWhiteSpace(criteria.Keywords))
            {
                conditions.Add(string.Format(Sql.ContainsFilter, "{0}"));
            }

            if (criteria.StyleId != null)
            {
                conditions.Add(string.Format(Sql.StyleFilter, "{2}"));
            }

            if (criteria.TeacherId != null)
            {
                conditions.Add(string.Format(Sql.TeacherFilter, "{4}"));
            }

            if (criteria.TeacherServiceId != null)
            {
                conditions.Add(string.Format(Sql.TeacherServiceFilter, "{5}"));
            }

            if (criteria.VenueId != null)
            {
                conditions.Add(string.Format(Sql.VenueFilter, "{6}"));
            }

            if (criteria.VenueTypeId != null)
            {
                conditions.Add(string.Format(Sql.VenueTypeFilter, "{7}"));
            }

            if (criteria.VenueServiceId != null)
            {
                conditions.Add(string.Format(Sql.VenueServiceFilter, "{8}"));
            }

            if (criteria.AccreditationBodyId != null)
            {
                conditions.Add(string.Format(Sql.AccreditationBodyFilter, "{9}"));
            }

            if (criteria.Date != null)
            {
                conditions.Add(string.Format(Sql.DateFilter, "{10}"));
            }

            if (criteria.Bounds != null)
            {
                conditions.Add(string.Format(Sql.BoundsFilter, "{12}", GeographicPoint.CoordinateSystem));
                boundsWkt = criteria.Bounds.ToWkt();
            }

            if (conditions.Count == 0) return new SearchResponse(new List<SearchResult>(), 0, false, false);

            conditions.Add(Sql.FriendshipBlockedFilter);

            string order = null;

            if (criteria.SortOrder == SearchSortOrder.Distance && criteria.Coordinates != null)
            {
                order = distanceExpression;
            }
            else if (criteria.SortOrder == SearchSortOrder.Name)
            {
                order = Sql.NameColumn;
            }
            else
            {
                order = Sql.DefaultOrder;
            }

            var orderByClause = string.Format(Sql.OrderByClause, order);

            string offsetFetchClause = null;

            if (criteria.PageSize > 0)
            {
                offsetFetchClause = string.Format(Sql.OffsetFetchClause, criteria.PageIndex * criteria.PageSize.Value, criteria.PageSize.Value + 1);
            }

            var whereClause = string.Format(Sql.WhereClause, string.Join(Sql.AndOperator, conditions.Select(v => "(" + v + ")")));

            var sql = string.Join(" ", new string[] { Sql.SelectClause, Sql.FromClause, whereClause, orderByClause, offsetFetchClause }.Where(e => e != null));

            var keywords = criteria.Keywords == null ? null : "\"" + criteria.Keywords.Replace("\"", "") + "\"";
            
            var results = Context.Database.SqlQuery<SearchResult>(
                sql,
                keywords,
                criteria.StyleId,
                criteria.TeacherId,
                criteria.TeacherServiceId,
                criteria.VenueId,
                criteria.VenueTypeId,
                criteria.VenueServiceId,
                criteria.AccreditationBodyId,
                criteria.Date,
                criteria.ProfileId,
                boundsWkt).ToList();

            int totalCount = 0;
            bool hasNextPage = false;
            bool hasPreviousPage = false;

            if (results.Count > 0)
            {
                totalCount = results[0].TotalCount;
            }
            else
            {
                totalCount = 0;
            }

            if (criteria.PageSize > 0)
            {
                if (results.Count > criteria.PageSize)
                {
                    results.Remove(results.Last());
                    hasNextPage = true;
                }

                if (criteria.PageIndex > 0)
                {
                    hasPreviousPage = true;
                }
            }

            return new SearchResponse(results, totalCount, hasNextPage, hasPreviousPage);
        }


        public void Index(ISearchable subject)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(ISearchable subject)
        {
            throw new System.NotImplementedException();
        }

        public void Index(Review review)
        {
            throw new System.NotImplementedException();
        }
    }
}