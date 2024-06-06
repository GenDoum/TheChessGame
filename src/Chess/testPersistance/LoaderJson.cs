using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using ChessLibrary;

namespace Persistance
{
    public class LoaderJson : IPersistanceManager
    {
        public string FileName { get; set; } = "data.json";

        public string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChessData");

        public (ObservableCollection<Game>, ObservableCollection<User>) LoadData()
        {
            if ( !File.Exists(Path.Combine(FilePath, FileName)) )
            {
                return (new ObservableCollection<Game>(), new ObservableCollection<User>());
            }

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DataToPersist));

            DataToPersist? data;

            using (FileStream fs = File.OpenRead(Path.Combine(FilePath, FileName)))
            {
                data = jsonSerializer.ReadObject(fs) as DataToPersist;
            }

            return (data!.games, data!.players);
        }

        public void SaveData(ObservableCollection<Game> games, ObservableCollection<User> players)
        {
            DataToPersist data = new DataToPersist();
            data.games = games;
            data.players = players;

            if(!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DataToPersist));
            using (FileStream fs = File.Create(Path.Combine(FilePath, FileName)))
            {
                using (var writter = JsonReaderWriterFactory.CreateJsonWriter(
                    fs, Encoding.UTF8, false, true))
                {
                    jsonSerializer.WriteObject(writter, data);
                    writter.Flush();
                }
            }
        }
    }
}