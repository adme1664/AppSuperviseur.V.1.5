using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ht.Ihsi.Rgph.DataAccess.Entities;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Data.Entity.Core.Objects;
using Ht.Ihsi.Rgph.DataAccess.Entities.MobileEntities;
using Ht.Ihsi.Rgph.DataAccess.Entities.SupEntities;

namespace Ht.Ihsi.Rgph.DataAccess.Repositories
{
    public class GenericRepository<TEntity> where TEntity : class
    {

            internal RgphContext context;
            internal GenericDatabaseContext databaseContext;
            internal GenericSupDatabaseContext supDatabaseContext;
            internal DbSet<TEntity> dbSet;

            public GenericRepository(RgphContext context)
            {
                this.context = context;
                this.dbSet = context.Set<TEntity>();
                
            }
            public GenericRepository(GenericDatabaseContext databaseContext)
            {
                this.databaseContext = databaseContext;
                this.dbSet = databaseContext.Set<TEntity>();
            }

            public GenericRepository(GenericSupDatabaseContext context)
            {
                this.supDatabaseContext = context;
                this.dbSet = supDatabaseContext.Set<TEntity>();
            }

            /// <summary>
            ///     Rechercher un enregistrement dans une entite par sa cle primaire
            /// </summary>
            /// <param name="id"></param>
            /// <returns></returns>
            public virtual TEntity FindOne(object id)
            {
                return dbSet.Find(id);
            }

            /// <summary>
            /// Ajouter un nouvel entite dans le Context
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public virtual TEntity Insert(TEntity entity)
            {
                dbSet.Add(entity);
                return entity;
            }

            public virtual int CommandSqlString(string sqlString)
            {
                int rowInsert=context.Database.ExecuteSqlCommand(sqlString);
                return rowInsert;
            }
            

            /// <summary>
            /// Supprimer une entite dans le context par son identifiant
            /// </summary>
            /// <param name="id"></param>
            public virtual void Delete(object id)
            {
                TEntity entityToDelete = dbSet.Find(id);
                Delete(entityToDelete);
            }

            /// <summary>
            /// Supprimer une entitie dans le context
            /// </summary>
            /// <param name="entityToDelete"></param>
            public virtual void Delete(TEntity entityToDelete)
            {
                if (supDatabaseContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }

            /// <summary>
            /// Mettre ajour une entite dans le context
            /// </summary>
            /// <param name="entityToUpdate"></param>
            public virtual void Update(TEntity entityToUpdate)
            {
                dbSet.Attach(entityToUpdate);
                supDatabaseContext.Entry(entityToUpdate).State = EntityState.Modified;
               // context.Entry(entityToUpdate).Reload();

            }
            /// <summary>
            /// Supper une entitie dans le context
            /// </summary>
            /// <param name="entityToDelete"></param>
            public virtual void DeleteGB(TEntity entityToDelete)
            {
                if (databaseContext.Entry(entityToDelete).State == EntityState.Detached)
                {
                    dbSet.Attach(entityToDelete);
                }
                dbSet.Remove(entityToDelete);
            }

            /// <summary>
            /// Mettre une entite dans le context
            /// </summary>
            /// <param name="entityToUpdate"></param>
            public virtual void UpdateGB(TEntity entityToUpdate)
            {
                dbSet.Attach(entityToUpdate);
                databaseContext.Entry(entityToUpdate).State = EntityState.Modified;
                // context.Entry(entityToUpdate).Reload();

            }
            /// <summary>
            /// Recherche suivant des criteres sur une entite
            /// </summary>
            /// <param name="filter"></param>
            /// <param name="orderBy"></param>
            /// <param name="includeProperties"></param>
            /// <returns></returns>
            public virtual IEnumerable<TEntity> Find(
                Expression<Func<TEntity, bool>> filter = null,
                Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                string includeProperties = "")
            {
                IQueryable<TEntity> query = dbSet;
                
                if (filter != null)
                {
                    query = query.Where(filter);
                }

                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }

                if (orderBy != null)
                {
                    return orderBy(query).ToList();
                }
                else
                {
                    return query.ToList();
                }
            }
         
        }
    }

