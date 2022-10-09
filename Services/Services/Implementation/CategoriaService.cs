using AutoMapper;
using Domain.Entities;
using Persistence.Repositories.Interfaces;
using Services.DTO;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Implementation
{
    public class CategoriaService : ServiceBase, ICategoriaService
    {
        public CategoriaService(IRepositoryWrapper repository, IMapper mapper, IServiceWrapper serviceWrapper) : base(repository, mapper, serviceWrapper)
        {
        }

        public void CreateCategoria(CategoriaDTO categoria)
        {
            var categoriaCriar = _mapper.Map<Categoria>(categoria);

            categoriaCriar.Gerar();

            _repository.CategoriaRepository.Create(categoriaCriar);
            _repository.Save();
        }

        public void DeleteCategoria(Guid id)
        {
            _repository.CategoriaRepository.DeleteById(id);
            _repository.Save();
        }

        public CategoriaDTO GetCategoria(Guid id)
        {
            var categoria = _repository.CategoriaRepository.FindById(id).FirstOrDefault();

            return _mapper.Map<CategoriaDTO>(categoria);
        }

        public List<CategoriaDTO> GetCategorias()
        {
            var categorias = _repository.CategoriaRepository.GetAll().ToList();

            return _mapper.Map<List<CategoriaDTO>>(categorias);
        }

        public void UpdateCategoria(CategoriaDTO categoria)
        {
            var categoriaEncontrada = _repository.CategoriaRepository.FindById(categoria.Id.GetValueOrDefault()).FirstOrDefault();

            categoriaEncontrada.Nome = categoria.Nome;
            categoriaEncontrada.Cor = categoria.Cor;

            _repository.CategoriaRepository.Update(categoriaEncontrada);
            _repository.Save();
        }
    }
}
