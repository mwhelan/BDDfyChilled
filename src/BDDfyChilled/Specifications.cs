using Chill;
using NUnit.Framework;
using TestStack.BDDfy;
using Xunit;

    namespace BDDfyChilled.xUnit
    {
        public abstract class SpecificationFor<TSubject> : GivenSubject<TSubject> where TSubject : class
        {
            [Fact]
            public void Run()
            {
                this.BDDfy();
            }
        }

        public abstract class SpecificationFor<TSubject, TResult> : GivenSubject<TSubject, TResult> where TSubject : class
        {
            [Fact]
            public void Run()
            {
                this.BDDfy();
            }
        }
    }
    namespace BDDfyChilled.NUnit
    {
        public abstract class SpecificationFor<TSubject> : GivenSubject<TSubject> where TSubject : class
        {
            [Test]
            public void Run()
            {
                this.BDDfy();
            }
        }
        public abstract class SpecificationFor<TSubject, TResult> : GivenSubject<TSubject, TResult> where TSubject : class
        {
            [Test]
            public void Run()
            {
                this.BDDfy();
            }
        }
    }
