﻿using BigBall.Common.Entity;
using BigBall.Common.Entity.DBInterfaces;
using Moq;

namespace BigBall.Context.Tests
{
    internal class TestWriterContext : IDbWriterContext
    {
        public IDbWriter Writer { get; }
        public IUnitOfWork UnitOfWork { get; }
        public IDateTimeProvider DateTimeProvider { get; }
        public string UserName { get; }

        public TestWriterContext(IDbWriter writer,
            IUnitOfWork unitOfWork)
        {
            Writer = writer;
            UnitOfWork = unitOfWork;

            var dateTimeProviderMock = new Mock<IDateTimeProvider>();
            dateTimeProviderMock.Setup(x => x.UtcNow).Returns(DateTimeOffset.UtcNow);
            DateTimeProvider = dateTimeProviderMock.Object;
            UserName = "UserForTests";
        }
    }
}