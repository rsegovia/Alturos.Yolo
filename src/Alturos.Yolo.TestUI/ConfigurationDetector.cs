﻿using System.IO;
using System.Linq;

namespace Alturos.Yolo.TestUI
{
    public class ConfigurationDetector
    {
        public YoloConfiguration Detect()
        {
            var files = this.GetYoloFiles();
            var yoloConfiguration = this.MapFiles(files);
            var configValid = this.AreYoloFilesValid(yoloConfiguration);

            if (configValid)
            {
                return yoloConfiguration;
            }

            return null;
        }

        private string[] GetYoloFiles()
        {
            return Directory.GetFiles(".", "*.*", SearchOption.TopDirectoryOnly).Where(o => o.EndsWith(".names") || o.EndsWith(".cfg") || o.EndsWith(".weights")).ToArray();
        }

        private YoloConfiguration MapFiles(string[] files)
        {
            var configurationFile = files.Where(o => o.EndsWith(".cfg")).FirstOrDefault();
            var weightsFile = files.Where(o => o.EndsWith(".weights")).FirstOrDefault();
            var namesFile = files.Where(o => o.EndsWith(".names")).FirstOrDefault();

            return new YoloConfiguration(configurationFile, weightsFile, namesFile);
        }

        private bool AreYoloFilesValid(YoloConfiguration config)
        {
            if (string.IsNullOrEmpty(config.ConfigFile) ||
                string.IsNullOrEmpty(config.WeightsFile) ||
                string.IsNullOrEmpty(config.NamesFile))
            {
                return false;
            }

            if (Path.GetFileNameWithoutExtension(config.ConfigFile) != Path.GetFileNameWithoutExtension(config.WeightsFile))
            {
                return false;
            }

            return true;
        }
    }
}
