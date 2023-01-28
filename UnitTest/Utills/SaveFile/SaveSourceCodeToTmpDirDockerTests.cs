using Xunit;
using Api.Utills.SaveFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Api.Utills.Execution;

namespace Api.Utills.SaveFile.Tests
{
    public class SaveSourceCodeToTmpDirDockerTests
    {
        [Fact()]
        public void HashStringTest()
        {
            var fileStrategy = SaveSourceCodeToTmpDirDocker.Instant;
            var text = "Anirut";
            var salt = "Chaogla";

            var expected = fileStrategy.HashString(text);
            var expectedWithSalt = fileStrategy.HashString(text, salt);

            expectedWithSalt.Should().Be("9F299D1A7CF2AC70EA4D41210049CD480DD2AFB39741D85DF40B43506197F2F2");
            expected.Should().Be("6DAF06779CF4849C38F504A8B8529320275CAD1D75A4DCD67503786C33E8D017");
        }

        [Fact()]
        public void SaveTest()
        { 
            var fileStrategy = SaveSourceCodeToTmpDirDocker.Instant;
            var sourceCode = new SourceCode();
            var fileExtension = "TestSaveFile";

            fileStrategy.Save(sourceCode, out string filename, fileExtension);

            filename.Should().Be($"code_.{fileExtension}");
        }
    }
}