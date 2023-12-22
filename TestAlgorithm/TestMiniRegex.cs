using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algorithm;

namespace TestAlgorithm
{
    [TestClass]
    public class TestMiniRegex
    {

        [TestMethod]
        [Timeout(1000)]
        public void Test()
        {
            {
                // Test Case 1: Basic Meta Characters
                MiniRegex reg = new MiniRegex("abc");
                Assert.IsTrue(reg.IsMatch("abc")); // Expected: Match
            }
            {
                // Test Case 2: Meta Characters with Quantifiers
                MiniRegex reg = new MiniRegex("a+");
                Assert.IsTrue(reg.IsMatch("aaa")); // Expected: Match
            }

            {
                // Test Case 3: Character Set
                MiniRegex reg = new MiniRegex("[aeiou]");
                Assert.IsTrue(reg.IsMatch("o")); // Expected: Match
            }

            {
                // Test Case 4: Character Set with Quantifier
                MiniRegex reg = new MiniRegex("[aeiou]+");
                Assert.IsTrue(reg.IsMatch("aei")); // Expected: Match
            }

            {
                // Test Case 5: Negated Character Set
                MiniRegex reg = new MiniRegex("[^aeiou]");
                Assert.IsTrue(reg.IsMatch("x")); // Expected: Match
            }

            {
                // Test Case 6: Character Range
                MiniRegex reg = new MiniRegex("[a-z]");
                Assert.IsTrue(reg.IsMatch("m")); // Expected: Match
            }

            {
                // Test Case 7: Quantifier Range
                MiniRegex reg = new MiniRegex("a{2,4}");
                Assert.IsTrue(reg.IsMatch("aaa")); // Expected: Match
            }

            {
                // Test Case 8: Grouping
                MiniRegex reg = new MiniRegex("(abc)+");
                Assert.IsTrue(reg.IsMatch("abcabc")); // Expected: Match
            }

            {
                // Test Case 9: Meta Characters and Escaping
                MiniRegex reg = new MiniRegex(@"\d\s\w");
                Assert.IsTrue(reg.IsMatch("1 A")); // Expected: Match
            }

            {
                // Test Case 10: Complex Pattern
                MiniRegex reg = new MiniRegex(@"(\d{2,3}[a-z]){2}");
                Assert.IsTrue(reg.IsMatch("12b45c")); // Expected: Match
            }

            {
                // Test Case 11: Alternation
                MiniRegex reg = new MiniRegex("cat|dog");
                Assert.IsTrue(reg.IsMatch("dog")); // Expected: Match
            }

            {
                // Test Case 12: Escaping Special Characters
                MiniRegex reg = new MiniRegex(@"\[\d{3}\]");
                Assert.IsTrue(reg.IsMatch("[123]")); // Expected: Match
            }

            {
                // Test Case 13: Multiple Quantifiers
                MiniRegex reg = new MiniRegex(@"\w{3,5}\d{2,3}");
                Assert.IsTrue(reg.IsMatch("abc123")); // Expected: Match
            }

            {
                // Test Case 14: Grouping and Quantifiers
                MiniRegex reg = new MiniRegex(@"(ab)+\d{2}");
                Assert.IsTrue(reg.IsMatch("abab12")); // Expected: Match
            }

            {
                // Test Case 15: Complex Combination
                MiniRegex reg = new MiniRegex(@"(\w{2}\d)+[aeiou]+");
                Assert.IsTrue(reg.IsMatch("ab12aeiou")); // Expected: Match
            }

            {
                // Test Case 16: Nested Grouping
                MiniRegex reg = new MiniRegex(@"(\d{2,3}[a-z]+){2,3}");
                Assert.IsTrue(reg.IsMatch("12xyz34abc567defg")); // Expected: Match
            }

            {
                // Test Case 17: Start and End Anchors
                MiniRegex reg = new MiniRegex(@"^\d{3}-\w{2}$");
                Assert.IsTrue(reg.IsMatch("123-AB")); // Expected: Match
            }

            {
                // Test Case 18: Word Boundary
                MiniRegex reg = new MiniRegex(@"\w+word\b");
                Assert.IsTrue(reg.IsMatch("keyword")); // Expected: Match
            }


            {
                //19
                MiniRegex reg = new MiniRegex(@"[ \t\r\n]*\w+\s*=\s*[0-9]{1,}\s*");
                Assert.IsTrue(reg.IsMatch("    aabb   =  9988 "));
            }

            {
                //20
                MiniRegex reg = new MiniRegex(@"(-\d{2,3}-\d{3})+");
                Assert.IsFalse(reg.IsMatch("-11-222-333-44")); //false
                //21
                Assert.IsTrue(reg.IsMatch("-11-222-333-555")); //true
            }

            {
                //22
                MiniRegex reg = new MiniRegex(@"[a-f]*abc");
                bool ret = reg.IsMatch("abcdefabcdefabc");
                Assert.IsTrue(ret);
            }

            {
                //23
                MiniRegex reg = new MiniRegex(@"a*a*a*a*a*a*a*a*a*a*aaaaaab");
                bool ret = reg.IsMatch("aaaaaaaaaaaaaaaaaaaaaaaaaaaab");
                Assert.IsTrue(ret);
            }

        }
    }
}
