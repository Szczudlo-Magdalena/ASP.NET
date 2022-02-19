using LibApp.Dtos;
using LibApp.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace LibApp.Data
{
    public class RestApiClient
    {
        private readonly HttpClient http;
        private readonly string rootUrl;

        public RestApiClient(string rootUrl)
        {
            if (rootUrl.EndsWith("/"))
            {
                rootUrl = rootUrl.Substring(0, rootUrl.Length - 1);
            }

            this.rootUrl = rootUrl;
            http = new HttpClient();
        }

        public async Task<List<BookDto>> GetAllBooks()
        {
            return await DoRequestAsync<List<BookDto>>("/books", HttpMethod.Get);
        }

        public async Task<BookDto> GetOneBook(int id)
        {
            return await DoRequestAsync<BookDto>($"/books/{id}", HttpMethod.Get);
        }

        public async Task<BookDto> AddBook(BookDto book)
        {
            return await DoRequestAsync<BookDto>("/books", HttpMethod.Post, book);
        }

        public async Task<BookDto> UpdateBook(int id, BookDto book)
        {
            return await DoRequestAsync<BookDto>($"/books/{id}", HttpMethod.Put, book);
        }

        public async Task DeleteBook(int id)
        {
            await DoRequestAsync<BookDto>($"/books/{id}", HttpMethod.Delete);
        }

        public async Task<List<GenreDto>> GetAllGenres()
        {
            return await DoRequestAsync<List<GenreDto>>("/genres", HttpMethod.Get);
        }

        public async Task<List<CustomerDto>> GetAllCustomers()
        {
            return await DoRequestAsync<List<CustomerDto>>("/customers", HttpMethod.Get);
        }

        public async Task<CustomerDto> GetOneCustomer(int id)
        {
            return await DoRequestAsync<CustomerDto>($"/customers/{id}", HttpMethod.Get);
        }

        public async Task<CustomerDto> AddCustomer(CustomerDto customer)
        {
            return await DoRequestAsync<CustomerDto>("/customers", HttpMethod.Post, customer);
        }

        public async Task<CustomerDto> UpdateCustomer(int id, CustomerDto customer)
        {
            return await DoRequestAsync<CustomerDto>($"/customers/{id}", HttpMethod.Put, customer);
        }

        public async Task DeleteCustomer(int id)
        {
            await DoRequestAsync<CustomerDto>($"/customers/{id}", HttpMethod.Delete);
        }

        public async Task<NewRentalDto> AddNewRental(NewRentalDto newRental)
        {
            return await DoRequestAsync<NewRentalDto>("/newrentals", HttpMethod.Post, newRental);
        }

        public async Task<List<MembershipTypeDto>> GetAllMembershipTypes()
        {
            return await DoRequestAsync<List<MembershipTypeDto>>("/membershiptypes", HttpMethod.Get);
        }

        private async Task<T> DoRequestAsync<T>(string url, HttpMethod method, object content = null)
        {
            HttpRequestMessage request = new HttpRequestMessage(method, rootUrl + url);

            if (content != null)
            {
                request.Content = JsonContent.Create(content);
            }

            using HttpResponseMessage response = await http.SendAsync(request);
            string json = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException(
                    $"Invalid status code {response.StatusCode} at url {url}. Response: {json}");
            }

            T result = JsonConvert.DeserializeObject<T>(json);

            return result;
        }
    }
}
