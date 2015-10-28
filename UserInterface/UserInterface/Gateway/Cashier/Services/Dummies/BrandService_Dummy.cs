using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UserInterface.MasterData;

namespace UserInterface.Helpers.Services.MasterData
{
    public class BrandService_Dummy : IBrandService
    {
        public List<BrandDto> BrandDtos { get; set; }

        public BrandService_Dummy()
        {
            string jsonData = string.Empty;

            jsonData = jsonData + "[";
            jsonData = jsonData + "  {";
            jsonData = jsonData + "    \"id\": \"a872e347-19c5-4a7c-b6b6-7361e994341d\",";
            jsonData = jsonData + "    \"title\": \"TRIPSCH\",";
            jsonData = jsonData + "    \"code\": \"Estes Cash\",";
            jsonData = jsonData + "    \"description\": \"Dolor id elit aliquip aliquip labore labore elit id minim laborum fugiat pariatur quis dolor. Aliquip ullamco velit non dolor magna excepteur non occaecat est. Deserunt ut elit adipisicing laborum nisi ullamco aliquip quis veniam. Ut labore aliqua labore id.\r\n\"";
            jsonData = jsonData + "  },";
            jsonData = jsonData + "  {";
            jsonData = jsonData + "    \"id\": \"bf37b69b-8ecf-4576-9eb5-37e4734167cb\",";
            jsonData = jsonData + "    \"title\": \"TERAPRENE\",";
            jsonData = jsonData + "    \"code\": \"Harrison Campos\",";
            jsonData = jsonData + "    \"description\": \"Cillum in ipsum eu occaecat. Nulla nisi pariatur fugiat eiusmod tempor aliquip ullamco occaecat est. Adipisicing velit commodo laborum velit eu Lorem irure. Commodo do proident aliqua laborum veniam Lorem velit. Ea eu quis sit cillum voluptate nisi magna adipisicing consectetur.\r\n\"";
            jsonData = jsonData + "  },";
            jsonData = jsonData + "  {";
            jsonData = jsonData + "    \"id\": \"21eb958d-e5b5-4214-b4ad-53fc076d4e71\",";
            jsonData = jsonData + "    \"title\": \"EXPOSA\",";
            jsonData = jsonData + "    \"code\": \"Lenora Clarke\",";
            jsonData = jsonData + "    \"description\": \"Veniam eu qui cillum anim consectetur irure id sunt. Elit labore aliquip ipsum deserunt minim aute tempor. Dolore enim id quis exercitation mollit velit ex sunt nulla exercitation exercitation sit.\r\n\"";
            jsonData = jsonData + "  },";
            jsonData = jsonData + "  {";
            jsonData = jsonData + "    \"id\": \"7baaa7ee-c71a-4f5f-a878-21ec0fe0a3b4\",";
            jsonData = jsonData + "    \"title\": \"QUIZKA\",";
            jsonData = jsonData + "    \"code\": \"Olson Alvarez\",";
            jsonData = jsonData + "    \"description\": \"Consectetur et consequat irure culpa adipisicing minim in ut labore consectetur id officia laborum mollit. Pariatur aliqua cupidatat in fugiat consequat. Sunt mollit in culpa consequat exercitation laborum enim elit amet voluptate quis sit excepteur sit. Fugiat aliqua consectetur sunt nostrud voluptate ex quis excepteur minim ut enim mollit est ex.\r\n\"";
            jsonData = jsonData + "  },";
            jsonData = jsonData + "  {";
            jsonData = jsonData + "    \"id\": \"bb3ea9b0-52fa-4da8-9f3a-61b63421f6a1\",";
            jsonData = jsonData + "    \"title\": \"OPTICOM\",";
            jsonData = jsonData + "    \"code\": \"Sheri Olsen\",";
            jsonData = jsonData + "    \"description\": \"Aliquip sint magna dolor velit aliqua ut aliquip eu aute sint magna nulla officia et. Adipisicing aliquip incididunt mollit magna laboris elit eu mollit veniam dolore nisi deserunt. Eiusmod elit veniam adipisicing ullamco culpa minim aute occaecat. Incididunt esse est labore cillum amet et esse quis et aliquip ullamco ullamco enim. Voluptate incididunt ullamco nostrud nulla incididunt sunt elit. Esse consectetur adipisicing labore esse sunt ex adipisicing. Dolore exercitation non occaecat excepteur quis non aute occaecat.\r\n\"";
            jsonData = jsonData + "  }";
            jsonData = jsonData + "]";

            BrandDtos = JsonConvert.DeserializeObject<List<BrandDto>>(jsonData);
        }

        public async Task<IQueryable<BrandDto>> GetBrandListAsync()
        {
            var dtos = BrandDtos.AsEnumerable();
            return dtos.AsQueryable();
        }

        public async Task<BrandDto> GetBrandAsync(Guid id)
        {
            var dto = BrandDtos.FirstOrDefault(x => x.Id == id);
            return dto;
        }

        public async Task<CreateBrandResponse> CreateBrandAsync(CreateBrandCommand cmd)
        {
            throw new NotImplementedException();
        }

        public async Task<AlterBrandResponse> AlterBrandAsync(Guid id, AlterBrandCommand cmd)
        {
            throw new NotImplementedException();
        }
    }
}