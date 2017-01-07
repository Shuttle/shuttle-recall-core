﻿using System;
using System.ComponentModel;
using System.Configuration;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Recall
{
	public class EventStoreSection : ConfigurationSection
	{
        private static bool _initialized;
        private static readonly object Padlock = new object();
        private static EventStoreSection _section;

        [ConfigurationProperty("encryptionAlgorithm", IsRequired = false, DefaultValue = "")]
        public string EncryptionAlgorithm
        {
            get { return (string)this["encryptionAlgorithm"]; }
        }

        [ConfigurationProperty("compressionAlgorithm", IsRequired = false, DefaultValue = "")]
        public string CompressionAlgorithm
        {
            get { return (string)this["compressionAlgorithm"]; }
        }

        [TypeConverter(typeof(StringDurationArrayConverter))]
        [ConfigurationProperty("durationToSleepWhenIdle", IsRequired = false, DefaultValue = null)]
        public TimeSpan[] DurationToSleepWhenIdle
        {
            get { return (TimeSpan[])this["durationToSleepWhenIdle"]; }
        }

        public static EventStoreSection Get()
        {
            lock (Padlock)
            {
                if (!_initialized)
                {
                    _section = ConfigurationSectionProvider.Open<EventStoreSection>("shuttle", "eventStore");

                    _initialized = true;
                }

                return _section;
            }
        }
    }
}