// Made by AI: Claude Opus 4.5
using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace Hex.Tools
{
    /// <summary>
    /// Manages debug data snapshots and sessions.
    /// Made by AI: Claude Opus 4.5
    /// </summary>
    public static class DebugDataManager
    {
        private const string DEBUG_FOLDER = "_Debug";
        private static string _currentSessionId;
        private static List<SnapshotInfo> _currentSessionSnapshots = new List<SnapshotInfo>();
        
        // Made by AI: Claude Opus 4.5 - Changed to class for proper JSON serialization
        public class SnapshotInfo
        {
            public string SessionId { get; set; } = "";
            public string SnapshotId { get; set; } = "";
            public string FilePath { get; set; } = "";
            public DateTime Timestamp { get; set; }
            public string DisplayName { get; set; } = "";
        }
        
        // Made by AI: Claude Opus 4.5 - Changed to class for proper JSON serialization
        public class SnapshotData
        {
            public string SessionId { get; set; } = "";
            public string SnapshotId { get; set; } = "";
            public DateTime Timestamp { get; set; }
            public Logic.DataBuffer.BonusGiverBufferDebug.BonusGiverBufferDebugData BonusGiverData { get; set; }
            public Logic.DataBuffer.BonusStepBufferDebug.BonusStepBufferDebugData BonusStepData { get; set; }
            public Logic.DataBuffer.LocalStepBufferDebug.LocalStepBufferDebugData LocalStepData { get; set; }
        }
        
        public static string CurrentSessionId => _currentSessionId;
        public static IReadOnlyList<SnapshotInfo> CurrentSessionSnapshots => _currentSessionSnapshots;
        
        static DebugDataManager()
        {
            InitializeSession();
        }
        
        public static void InitializeSession()
        {
            _currentSessionId = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            _currentSessionSnapshots.Clear();
            EnsureDebugFolderExists();
        }
        
        private static string GetDebugFolderPath()
        {
            return ProjectSettings.GlobalizePath("res://" + DEBUG_FOLDER);
        }
        
        private static string GetSessionFolderPath(string sessionId)
        {
            return Path.Combine(GetDebugFolderPath(), sessionId);
        }
        
        private static void EnsureDebugFolderExists()
        {
            string debugPath = GetDebugFolderPath();
            if (!Directory.Exists(debugPath))
            {
                Directory.CreateDirectory(debugPath);
            }
            
            string sessionPath = GetSessionFolderPath(_currentSessionId);
            if (!Directory.Exists(sessionPath))
            {
                Directory.CreateDirectory(sessionPath);
            }
        }
        
        public static SnapshotInfo TakeSnapshot()
        {
            EnsureDebugFolderExists();
            
            string snapshotId = DateTime.Now.ToString("HH-mm-ss-fff");
            string fileName = $"snapshot_{snapshotId}.json";
            string filePath = Path.Combine(GetSessionFolderPath(_currentSessionId), fileName);
            
            var data = new SnapshotData
            {
                SessionId = _currentSessionId,
                SnapshotId = snapshotId,
                Timestamp = DateTime.Now,
                BonusGiverData = Logic.DataBuffer.BonusGiverBufferDebug.GetDebugData(),
                BonusStepData = Logic.DataBuffer.BonusStepBufferDebug.GetDebugData(),
                LocalStepData = Logic.DataBuffer.LocalStepBufferDebug.GetDebugData()
            };
            
            // Serialize and save
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(filePath, json);
            
            var info = new SnapshotInfo
            {
                SessionId = _currentSessionId,
                SnapshotId = snapshotId,
                FilePath = filePath,
                Timestamp = data.Timestamp,
                DisplayName = $"Snapshot {_currentSessionSnapshots.Count + 1} ({snapshotId})"
            };
            
            _currentSessionSnapshots.Add(info);
            
            GD.Print($"[DebugDataManager] Snapshot saved: {filePath}");
            return info;
        }
        
        public static SnapshotData LoadSnapshot(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    GD.PrintErr($"[DebugDataManager] Snapshot file not found: {filePath}");
                    return null;
                }
                
                string json = File.ReadAllText(filePath);
                var data = JsonSerializer.Deserialize<SnapshotData>(json);
                return data;
            }
            catch (Exception ex)
            {
                GD.PrintErr($"[DebugDataManager] Error loading snapshot: {ex.Message}");
                return null;
            }
        }
        
        public static List<string> GetAvailableSessions()
        {
            var sessions = new List<string>();
            string debugPath = GetDebugFolderPath();
            
            if (!Directory.Exists(debugPath))
                return sessions;
            
            foreach (var dir in Directory.GetDirectories(debugPath))
            {
                sessions.Add(Path.GetFileName(dir));
            }
            
            sessions.Sort();
            sessions.Reverse(); // Most recent first
            return sessions;
        }
        
        public static List<SnapshotInfo> LoadSessionSnapshots(string sessionId)
        {
            var snapshots = new List<SnapshotInfo>();
            string sessionPath = GetSessionFolderPath(sessionId);
            
            if (!Directory.Exists(sessionPath))
                return snapshots;
            
            var files = Directory.GetFiles(sessionPath, "snapshot_*.json");
            Array.Sort(files);
            
            int index = 1;
            foreach (var file in files)
            {
                string fileName = Path.GetFileNameWithoutExtension(file);
                string snapshotId = fileName.Replace("snapshot_", "");
                
                // Try to get timestamp from file
                DateTime timestamp = File.GetCreationTime(file);
                
                snapshots.Add(new SnapshotInfo
                {
                    SessionId = sessionId,
                    SnapshotId = snapshotId,
                    FilePath = file,
                    Timestamp = timestamp,
                    DisplayName = $"Snapshot {index} ({snapshotId})"
                });
                index++;
            }
            
            return snapshots;
        }
        
        public static void SetCurrentSessionSnapshots(List<SnapshotInfo> snapshots)
        {
            _currentSessionSnapshots.Clear();
            _currentSessionSnapshots.AddRange(snapshots);
        }
    }
}
