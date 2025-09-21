using BusinessLogic.Interfaces;
using DataAccounts.Entitys.MovieEntitys;
using BusinessLogic.Model.GenreModel;
using DataAccounts.Repositories.GenreRopository;
using AutoMapper;
using DataAccounts.Entitys.GenreEntitys;
using System.IO;
using DataAccounts.Entitys;

namespace BusinessLogic.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task CreateGenre(CreateGenreModel createModel)
        {
            await _genreRepository.CreateGenreAsync(new Genre(createModel.name) { Id = createModel.id});
        }

        public async Task DeleteGenre(DeleteGenreModel deleteModel)
        {
            await _genreRepository.DeleteGenreAsync(deleteModel.id);
        }

        public async Task<PageModel<GenreDto>> GetGenre(GetGenresListModel filterModel)
        {
            var genresPage = await _genreRepository.GetGenresAsync(filterModel);
            var genreDtos = _mapper.Map<List<GenreDto>>(genresPage.Items);

            return new PageModel<GenreDto>(
                currentPage: genresPage.CurrentPage,
                nextPage: genresPage.NextPage,
                totalCount: genresPage.TotalCount,
                items: genreDtos
            );
        }

    }
}
