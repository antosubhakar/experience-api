using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Doctrina.ExperienceApi.Client.Tests.IO
{
    public class ResponseWriterTests
    {
        [Fact]
        public void WriteJsonStream()
        {
            string data = @"Content-Type: multipart/mixed; boundary=-------------2060259533
X-Experience-API-Version: 1.0.3
Authorization: Basic YWRtaW5AZXhhbXBsZS5jb206ektSNGdrWU5IUDV0dkg=
host: localhost:52209
content-length: 1409
Connection: close


---------------2060259533
Content-Type:application/json

{""actor"":{""objectType"":""Agent"",""name"":""xAPI mbox"",""mbox"":""mailto: xapi @adlnet.gov""},""verb"":{""id"":""http://adlnet.gov/expapi/verbs/attended"",""display"":{""en-GB"":""attended"",""en-US"":""attended""}},""object"":{""objectType"":""Activity"",""id"":""http://www.example.com/meetings/occurances/34534""},""id"":""3c5ced30-cb0d-40ea-9036-4ace482e2009"",""attachments"":[{""usageType"":""http://adlnet.gov/expapi/attachments/signature"",""display"":{""en-US"":""Signed by the Test Suite""},""description"":{""en-US"":""Signed by the Test Suite""},""contentType"":""application/octet-stream"",""length"":497,""sha2"":""0e3c7a76614c554479b2276ccd3777cf29612497d8f0999d00bdf32ba3f0ef2d""}]}
            ---------------2060259533
Content - Type:application / octet - stream
Content - Transfer - Encoding:binary
X - Experience - API - Hash:0e3c7a76614c554479b2276ccd3777cf29612497d8f0999d00bdf32ba3f0ef2d

eyJhbGciOiJIUzI1NiJ9.eyJhY3RvciI6eyJvYmplY3RUeXBlIjoiQWdlbnQiLCJuYW1lIjoieEFQSSBtYm94IiwibWJveCI6Im1haWx0bzp4YXBpQGFkbG5ldC5nb3YifSwidmVyYiI6eyJpZCI6Imh0dHA6Ly9hZGxuZXQuZ292L2V4cGFwaS92ZXJicy9hdHRlbmRlZCIsImRpc3BsYXkiOnsiZW4tR0IiOiJhdHRlbmRlZCIsImVuLVVTIjoiYXR0ZW5kZWQifX0sIm9iamVjdCI6eyJvYmplY3RUeXBlIjoiQWN0aXZpdHkiLCJpZCI6Imh0dHA6Ly93d3cuZXhhbXBsZS5jb20vbWVldGluZ3Mvb2NjdXJhbmNlcy8zNDUzNCJ9LCJpZCI6IjNjNWNlZDMwLWNiMGQtNDBlYS05MDM2LTRhY2U0ODJlMjAwOSJ9.LFqxDP0oqdvCOTOKxQQbEmh6aGPVO1zXGfAtn1Xsl3I
-------------- - 2060259533--";
            
        }
    }
}
