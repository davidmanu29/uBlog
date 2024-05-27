using Moq;
using Microsoft.AspNetCore.Mvc;
using uBlog.API.Controllers;
using uBlog.API.Models.Domain;
using uBlog.API.Models.DTO;
using uBlog.API.Repositories.Interface;

namespace uBlog.API.Tests
{
    public class BlogPostsControllerTests
    {
        private readonly Mock<IBlogPostRepository> _mockBlogPostRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly BlogPostsController _controller;

        public BlogPostsControllerTests()
        {
            _mockBlogPostRepository = new Mock<IBlogPostRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _controller = new BlogPostsController(_mockBlogPostRepository.Object, _mockCategoryRepository.Object);
        }

        [Fact]
        public async Task CreateBlogPost_ReturnsOkResult_WithBlogPostDto()
        {
            // Arrange
            var categoryGuid = Guid.NewGuid();
            var request = new CreateBlogPostRequestDto
            {
                Author = "Author",
                Content = "Content",
                FeaturedImageUrl = "http://example.com/image.jpg",
                IsVisible = true,
                PublishedDate = DateTime.UtcNow,
                ShortDescription = "Short description",
                Title = "Title",
                UrlHandle = "url-handle",
                Categories = new Guid[] { categoryGuid }
            };

            var createdBlogPost = new BlogPost
            {
                Id = Guid.NewGuid(),
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            _mockBlogPostRepository.Setup(repo => repo.CreateAsync(It.IsAny<BlogPost>())).ReturnsAsync(createdBlogPost);

            // Act
            var result = await _controller.CreateBlogPost(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var blogPostDto = Assert.IsType<BlogPostDto>(okResult.Value);
            Assert.Equal(createdBlogPost.Author, blogPostDto.Author);
        }

        [Fact]
        public async Task GetAllBlogPosts_ReturnsOkResult_WithListOfBlogPostDto()
        {
            // Arrange
            var blogPosts = new List<BlogPost>
            {
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Author = "Author1",
                    Content = "Content1",
                    FeaturedImageUrl = "http://example.com/image1.jpg",
                    IsVisible = true,
                    PublishedDate = DateTime.UtcNow,
                    ShortDescription = "Short description 1",
                    Title = "Title 1",
                    UrlHandle = "url-handle-1",
                    Categories = new List<Category>()
                },
                new BlogPost
                {
                    Id = Guid.NewGuid(),
                    Author = "Author2",
                    Content = "Content2",
                    FeaturedImageUrl = "http://example.com/image2.jpg",
                    IsVisible = true,
                    PublishedDate = DateTime.UtcNow,
                    ShortDescription = "Short description 2",
                    Title = "Title 2",
                    UrlHandle = "url-handle-2",
                    Categories = new List<Category>()
                }
            };

            _mockBlogPostRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(blogPosts);

            // Act
            var result = await _controller.GetAllBlogPosts();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var blogPostDtos = Assert.IsType<List<BlogPostDto>>(okResult.Value);
            Assert.Equal(2, blogPostDtos.Count);
        }

        [Fact]
        public async Task GetBlogPostById_ReturnsOkResult_WithBlogPostDto()
        {
            // Arrange
            var blogPostId = Guid.NewGuid();
            var blogPost = new BlogPost
            {
                Id = blogPostId,
                Author = "Author",
                Content = "Content",
                FeaturedImageUrl = "http://example.com/image.jpg",
                IsVisible = true,
                PublishedDate = DateTime.UtcNow,
                ShortDescription = "Short description",
                Title = "Title",
                UrlHandle = "url-handle",
                Categories = new List<Category>()
            };

            _mockBlogPostRepository.Setup(repo => repo.GetByIdAsync(blogPostId)).ReturnsAsync(blogPost);

            // Act
            var result = await _controller.GetBlogPostById(blogPostId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var blogPostDto = Assert.IsType<BlogPostDto>(okResult.Value);
            Assert.Equal(blogPost.Id, blogPostDto.Id);
        }

        [Fact]
        public async Task DeleteBlogPost_ReturnsOkResult_WithBlogPostDto()
        {
            // Arrange
            var blogPostId = Guid.NewGuid();
            var blogPost = new BlogPost
            {
                Id = blogPostId,
                Author = "Author",
                Content = "Content",
                FeaturedImageUrl = "http://example.com/image.jpg",
                IsVisible = true,
                PublishedDate = DateTime.UtcNow,
                ShortDescription = "Short description",
                Title = "Title",
                UrlHandle = "url-handle",
                Categories = new List<Category>()
            };

            _mockBlogPostRepository.Setup(repo => repo.DeleteAsnyc(blogPostId)).ReturnsAsync(blogPost);

            // Act
            var result = await _controller.DeleteBlogPost(blogPostId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var blogPostDto = Assert.IsType<BlogPostDto>(okResult.Value);
            Assert.Equal(blogPost.Id, blogPostDto.Id);
        }

        [Fact]
        public async Task UpdateBlogPost_ReturnsOkResult_WithBlogPostDto()
        {
            // Arrange
            var blogPostId = Guid.NewGuid();
            var request = new UpdateBlogPostRequestDto
            {
                Author = "Updated Author",
                Content = "Updated Content",
                FeaturedImageUrl = "http://example.com/updated-image.jpg",
                IsVisible = true,
                PublishedDate = DateTime.UtcNow,
                ShortDescription = "Updated short description",
                Title = "Updated Title",
                UrlHandle = "updated-url-handle",
                Categories = new List<Guid> { Guid.NewGuid() }
            };

            var updatedBlogPost = new BlogPost
            {
                Id = blogPostId,
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle,
                Categories = new List<Category>()
            };

            _mockBlogPostRepository.Setup(repo => repo.UpdateAsync(It.IsAny<BlogPost>())).ReturnsAsync(updatedBlogPost);

            // Act
            var result = await _controller.UpdateBlogPost(blogPostId, request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var blogPostDto = Assert.IsType<BlogPostDto>(okResult.Value);
            Assert.Equal(updatedBlogPost.Title, blogPostDto.Title);
        }   
    }
}