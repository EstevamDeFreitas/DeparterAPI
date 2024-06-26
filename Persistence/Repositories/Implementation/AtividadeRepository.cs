﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Database;
using Persistence.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Implementation
{
    public class AtividadeRepository : EntityRepositoryBase<Atividade>, IAtividadeRepository
    {
        public AtividadeRepository(DeparterContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Atividade> FindFullById(Guid id)
        {
            return DbContext.Atividades.Include(x => x.AtividadeCategorias)
                                            .ThenInclude(x => x.Categoria)
                                        .Include(x => x.AtividadeFuncionarios)
                                            .ThenInclude(x => x.Funcionario)
                                        .Include(x => x.Atividades)
                                        .Include(x => x.AtividadeChecks)
                                        //.Include(x => x.Departamento)
                                        .Where(x => x.Id == id).AsNoTracking();
        }

        public IQueryable<Atividade> FindAllFull(bool? isAdminSearch, Guid funcionarioId)
        {
            return DbContext.Atividades.Include(x => x.AtividadeCategorias)
                                            .ThenInclude(x => x.Categoria)
                                        .Include(x => x.AtividadeFuncionarios)
                                            .ThenInclude(x => x.Funcionario)
                                        .Where(x => (isAdminSearch.HasValue && isAdminSearch == true)? true : x.AtividadeFuncionarios.Any(y => y.FuncionarioId == funcionarioId))
                                        .AsNoTracking();
        }

        public void UpdateDatabaseAtividadesStatus()
        {
            var connection = DbContext.Database.GetDbConnection();

            var command = connection.CreateCommand();

            command.CommandText = "UPDATE atividades SET status_tarefa = 3 WHERE dt_entrega < NOW() and status_tarefa != 2";
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
