﻿using System.IO;
using System.Threading.Tasks;
using NFluent;

namespace LASI.Content.Tests
{
    using Exceptions;
    using LASI.Content.FileTypes;
    using Fact = Xunit.FactAttribute;

    /// <summary>
    ///This is a test class for TaggedFileTest and is intended
    ///to contain all TaggedFileTest Unit Tests
    /// </summary>
    public class TaggedFileTest
    {
        private const string VALID_TAGGED_FILE_PATH = @"..\..\MockUserFiles\Test paragraph about house fires.tagged";
        private const string DIFFERENT_TYPE_FILE_PATH = @"..\..\MockUserFiles\Test paragraph about house fires.txt";


        /// <summary>
        ///A test for TaggedFile Constructor
        /// </summary>
        [Fact]
        public void TaggedFileConstructorTest1()
        {
            var target = new TaggedFile(VALID_TAGGED_FILE_PATH);
            var fileInfo = new System.IO.FileInfo(VALID_TAGGED_FILE_PATH);
            Check.That(target.FullPath).IsEqualTo(fileInfo.FullName);
        }
        [Fact]
        public void TaggedFileConstructorTest2()
        {
            Check.ThatCode(() => new TaggedFile(DIFFERENT_TYPE_FILE_PATH)).Throws<FileTypeWrapperMismatchException<TaggedFile>>();
        }
        [Fact]
        public void TaggedFileConstructorTest3()
        {
            var invalidPath = Directory.GetCurrentDirectory();//This should never be valid.
            Check.ThatCode(() => new TaggedFile(invalidPath)).Throws<FileNotFoundException>();
        }
        /// <summary>
        ///A test for LoadText
        /// </summary>
        [Fact]
        public void LoadTextTest()
        {
            var target = new TaggedFile(VALID_TAGGED_FILE_PATH);
            var expected = File.ReadAllText(VALID_TAGGED_FILE_PATH);
            var actual = target.LoadText();
            Check.That(actual).IsEqualTo(expected);
        }

        /// <summary>
        ///A test for LoadTextAsync
        /// </summary>
        [Fact]
        public async Task LoadTextAsyncTest()
        {
            var target = new TaggedFile(VALID_TAGGED_FILE_PATH);
            var expected = File.ReadAllText(VALID_TAGGED_FILE_PATH);
            var actual = target.LoadTextAsync();
            Check.That(expected).IsEqualTo(await actual);
        }
    }
}
