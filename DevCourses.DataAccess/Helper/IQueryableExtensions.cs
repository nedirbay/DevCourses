using DevCourses.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace DevCourses.DataAccess.Helper
{
    public static class IQueryableExtensions
    {
        public static async Task<PagedResult<T>> ToPagedResultAsync<T>(this IQueryable<T> query, QueryParameters parameters)
        {
            // 1. TOPLAM KAYIT SAYISINI AL (Filtrelemeden sonra, sayfalama yapmadan önce)
            int totalCount = await query.CountAsync();

            // 2. FİLTRELEME
            // Dinamik filtreleme için daha gelişmiş bir yapı kurulabilir,
            // ama bu örnekte basit bir anahtar-değer filtresi yapalım.
            // Projeniz için spesifik filtreler ekleyebilirsiniz. Örnek: Kullanıcıları lige göre filtreleme
            if (typeof(T) == typeof(User) && parameters.FilterBy?.ToLower() == "leagueid" && Guid.TryParse(parameters.FilterValue, out Guid leagueId))
            {
                query = (IQueryable<T>)((IQueryable<User>)query).Where(u => u.CurrentLeagueId == leagueId);
            }

            // 3. ARAMA
            // Sadece User modeli için örnek arama. Diğer modeller için de genişletilebilir.
            if (typeof(T) == typeof(User) && !string.IsNullOrWhiteSpace(parameters.SearchQuery))
            {
                var searchQuery = parameters.SearchQuery.Trim().ToLower();
                query = (IQueryable<T>)((IQueryable<User>)query).Where(u =>
                    u.UserName.ToLower().Contains(searchQuery) ||
                    u.FullName.ToLower().Contains(searchQuery)
                );
            }

            // 4. SIRALAMA
            // System.Linq.Dynamic.Core kütüphanesi sayesinde string ifadelerle sıralama yapabiliyoruz.
            // Bu, SQL injection'a karşı güvenlidir ve çok esnektir.
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                var sortOrder = parameters.SortDirection?.ToLower() == "desc" ? "descending" : "ascending";
                query = query.OrderBy($"{parameters.SortBy} {sortOrder}");
            }
            else
            {
                // Varsayılan sıralama (her zaman bir sıralama olmalı)
                query = query.OrderBy("CreatedAt descending");
            }

            // 5. SAYFALAMA
            var items = await query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize)
                .ToListAsync();

            // 6. SONUCU OLUŞTUR VE DÖNDÜR
            return new PagedResult<T>(items, totalCount, parameters.PageNumber, parameters.PageSize);
        }
    }
}
