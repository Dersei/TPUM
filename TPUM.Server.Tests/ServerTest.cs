using System;
using TPUM.Communication;
using TPUM.Communication.Requests;
using TPUM.Communication.Responses;
using TPUM.Communication.TransferModel;
using TPUM.Serialization;
using TPUM.Server.GUI;
using Xunit;

namespace TPUM.Server.Tests
{
    public class ServerTest
    {
        [Fact]
        public void ProcessDataGetAllGamesTest()
        {
            RequestGetAllGames value = new RequestGetAllGames();
            string stringValue = Serializer.Serialize(value);
            string processingResult = ServerProcessing.ProcessData(stringValue, null!);
            ResponseGetAllGames deserialized = Serializer.Deserialize<ResponseGetAllGames>(processingResult);
            Assert.NotNull(deserialized);
            Assert.Equal(8, deserialized.Games!.Count);
        }

        [Fact]
        public void ProcessDataLogInTest()
        {
            RequestLogIn value = new RequestLogIn
            {
                Credentials = new TransferUser(Guid.Empty, "q", "q")
            };
            string stringValue = Serializer.Serialize(value);
            string processingResult = ServerProcessing.ProcessData(stringValue, null!);
            ResponseLogIn deserialized = Serializer.Deserialize<ResponseLogIn>(processingResult);
            Assert.NotNull(deserialized);
            Assert.Equal(HashCode.Combine("q", "q"), deserialized.Token.Value);
        }

        [Fact]
        public void ProcessDataCreateGameTest()
        {
            RequestCreateGame value = new RequestCreateGame()
            {
                Game = new TransferGame("TGame", new TransferPublisher("T", "US"), 10, DateTime.Now,
                        (uint)Genre.FPS)
            };
            string stringValue = Serializer.Serialize(value);
            string processingResult = ServerProcessing.ProcessData(stringValue, null!);
            ResponseCreateGame deserialized = Serializer.Deserialize<ResponseCreateGame>(processingResult);
            Assert.NotNull(deserialized);
            Assert.True(deserialized.Success);
            Assert.NotNull(deserialized.CreatedGame);
        }

        [Fact]
        public void ProcessDataFailTest()
        {
            Interchange value = new Interchange();
            string stringValue = Serializer.Serialize(value);
            string processingResult = ServerProcessing.ProcessData(stringValue, null!);
            Interchange deserialized = Serializer.Deserialize<Interchange>(processingResult);
            Assert.NotNull(deserialized);
            Assert.Equal(InterchangeStatus.Fail, deserialized.Status);
        }
    }
}
