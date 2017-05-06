using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;

namespace BlueSheep.Common.Data.D2o
{
    public class GameData
    {
        // Methods
        /// <summary>Retorna os dados de um elemento do jogo através do nome do recipiente e o identificador do elemento</summary>
        /// <param name="ModuleName">Nome do recipiente (sans le .D2o)</param>
        /// <param name="Id">Identificador do elemento</param>
        /// <returns>Dados do elemento</returns>
        public static DataClass GetDataObject(D2oFileEnum ModuleName, int Id)
        {
            string File = ModuleName.ToString();
            lock (GameData.CheckLock)
            {
                if (GameData.FileName_Data.ContainsKey(File))
                {
                    return GameData.FileName_Data[File].DataObject(File, Id);
                }
                return null;
            }
        }

        /// <summary>Recupera todos os dados do recipiente</summary>
        /// <param name="ModuleName">Nome do recipiente(sans le .D2o)</param>
        /// <returns>Os dados do recipiente</returns>
        public static DataClass[] GetDataObjects(D2oFileEnum ModuleName)
        {
            string File = ModuleName.ToString();
            lock (GameData.CheckLock)
            {
                return GameData.FileName_Data[File].DataObjects(File);
            }
        }

        /// <summary>Inicializa os dados</summary>
        /// <param name="DirectoryInit">Caminho para o diretório que contém o .d2o</param>
        /// <remarks>Este método executa automaticamente no momento da inicialização do updater</remarks>
        public static void Init(string DirectoryInit)
		{
			GameData.FileName_Data = new Dictionary<string, D2oData>();
			foreach (string fichier_loopVariable in Directory.GetFiles(DirectoryInit)) {
				string fichier = fichier_loopVariable;
				FileInfo info = new FileInfo(fichier);
				if ((info.Extension.ToUpper() == ".D2O")) {
					D2oData D2oData = new D2oData(fichier);
					GameData.FileName_Data.Add(Path.GetFileNameWithoutExtension(fichier), D2oData);
				}
			}
		}

        // Fields
        static internal Dictionary<string, D2oData> FileName_Data;
        private static object CheckLock = RuntimeHelpers.GetObjectValue(new object());
    }
}