
using AutoMapper.Internal.Mappers;
using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Dtos;
using MovieApp.Core.Repositories;
using MovieApp.Core.Services;
using MovieApp.Core.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.Service.Services
{
    public class GenericService<TEntity, TDto> : IServiceGeneric<TEntity, TDto> where TEntity : class where TDto : class
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<TEntity> _genericRepository;
        public GenericService(IUnitOfWork unitOfWork, IGenericRepository<TEntity> genericRepository)
        {
            _unitOfWork = unitOfWork;
            _genericRepository = genericRepository;
        }

        public async Task<ResponseDto<TDto>> AddAsync(TDto entity)
        {
            var newEntity = ObejctMapper.Mapper.Map<TEntity>(entity);

            await _genericRepository.AddAsync(newEntity);
            await _unitOfWork.CommitAsync();

            var newDto = ObejctMapper.Mapper.Map<TDto>(newEntity);
            return ResponseDto<TDto>.Success(newDto, 200);
        }

        public async Task<ResponseDto<IEnumerable<TDto>>> GetAllAsync()
        {
            var entities = ObejctMapper.Mapper.Map<List<TDto>>(await _genericRepository.GetAllAsync());
            return ResponseDto<IEnumerable<TDto>>.Success(entities, 200);
        }

        public async Task<ResponseDto<TDto>> GetByIdAsync(int id)
        {
            var entity = await _genericRepository.GetByIdAsync(id);
            if(entity == null)
            {
                return ResponseDto<TDto>.Fail("id not found", 404, true);
            }
            return ResponseDto<TDto>.Success(ObejctMapper.Mapper.Map<TDto>(entity), 200);
        }

        public async Task<ResponseDto<NoDataDto>> Remove(int id)
        {
            var ixExistEntity = await _genericRepository.GetByIdAsync(id);
            if(ixExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("id not found", 404, true);

            }

            _genericRepository.Remove(ixExistEntity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success(200);
        }

        public async Task<ResponseDto<NoDataDto>> Update(TDto entity, int id)
        {
            var ixExistEntity = await _genericRepository.GetByIdAsync(id);
            if (ixExistEntity == null)
            {
                return ResponseDto<NoDataDto>.Fail("id not found", 404, true);
            }
            var updateEntity = ObejctMapper.Mapper.Map<TEntity>(entity);
            _genericRepository.Update(updateEntity);
            await _unitOfWork.CommitAsync();
            return ResponseDto<NoDataDto>.Success(204);

        }

        public async Task<ResponseDto<IEnumerable<TDto>>> Where(Expression<Func<TEntity, bool>> predicate)
        {
            var list = _genericRepository.Where(predicate);
            return ResponseDto<IEnumerable<TDto>>.Success(ObejctMapper.Mapper.Map<IEnumerable<TDto>>(await list.ToListAsync()), 200);
        }
    }
}
