using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.Serialization;
using System.Xml;
using ChessLibrary;


namespace Persistance
{
    public class LoaderXML : IPersistanceManager
    {
        public string FileName { get; set; } = "data.xml";

        public string FilePath { get; set; } = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ChessData");


        public (ObservableCollection<Game>, ObservableCollection<User>, ObservableCollection<Chessboard>) LoadData()
        {
            var serializer = new DataContractSerializer(typeof(DataToPersist));
            DataToPersist? data;

            if (File.Exists(Path.Combine(FilePath, FileName)))
            {
                using (Stream s = File.OpenRead(Path.Combine(FilePath, FileName)))
                {
                    data = serializer.ReadObject(s) as DataToPersist;
                }
            }
            else
            {
                data= new DataToPersist();
            }

            return (data!.games, data!.players, data!.chessboards);
        }

        public void SaveData(ObservableCollection<Game> games, ObservableCollection<User> players, ObservableCollection<Chessboard> chessboards)
        {
            var serializer = new DataContractSerializer(typeof(DataToPersist));

            if(!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }

            DataToPersist data = new DataToPersist();
            data.games = games;
            data.players = players;
            data.chessboards = chessboards;

            var settings = new XmlWriterSettings
            {
                Indent = true,
            };

            using(TextWriter w = File.CreateText(Path.Combine(FilePath, FileName)))
            {
                using(XmlWriter writer = XmlWriter.Create(w, settings))
                {
                    serializer.WriteObject(writer, data);
                }
            }
        }
    }
}