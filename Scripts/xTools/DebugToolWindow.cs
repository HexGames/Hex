// Made by AI: Claude Opus 4.5
using Godot;
using System;
using System.Collections.Generic;

namespace Hex.Tools
{
    /// <summary>
    /// Debug tool window that displays buffer data in a tree-like hierarchy.
    /// Opens with F1 key. Made by AI: Claude Opus 4.5
    /// </summary>
    public partial class DebugToolWindow : Window
    {
        [Export]
        private TabContainer TabContainer;
        
        [Export]
        private Tree BufferTree;
        
        [Export]
        private Button SnapshotButton;
        
        [Export]
        private Button PrevSnapshotButton;
        
        [Export]
        private Button NextSnapshotButton;
        
        [Export]
        private Label SnapshotLabel;
        
        [Export]
        private Button LoadSessionButton;
        
        [Export]
        private Panel WarningPanel;
        
        [Export]
        private Label WarningLabel;
        
        [Export]
        private OptionButton SessionSelector;
        
        private TreeItem _rootItem;
        private TreeItem _bonusGiverRoot;
        private TreeItem _bonusStepRoot;
        
        private bool _isRefreshing = false;
        
        // Made by AI: Claude Opus 4.5 - Snapshot management
        private List<DebugDataManager.SnapshotInfo> _loadedSnapshots = new List<DebugDataManager.SnapshotInfo>();
        private int _currentSnapshotIndex = -1;
        private string _loadedSessionId = null;
        private bool _isViewingOldSession = false;
        
        public override void _Ready()
        {
            // Set window properties
            Title = "Debug Tools (F1)";
            Exclusive = false;
            Unresizable = false;
            CloseRequested += OnCloseRequested;
            
            // Set up tabs - disable tabs 2-5 (indices 1-4)
            if (TabContainer != null)
            {
                for (int i = 1; i < TabContainer.GetTabCount(); i++)
                {
                    TabContainer.SetTabDisabled(i, true);
                    TabContainer.SetTabTitle(i, $"Future Feature {i}");
                }
                TabContainer.SetTabTitle(0, "Buffer Inspector");
            }
            
            // Initialize the tree
            InitializeTree();
            
            // Made by AI: Claude Opus 4.5 - Initialize snapshot system
            _loadedSessionId = DebugDataManager.CurrentSessionId;
            _isViewingOldSession = false;
            UpdateWarningVisibility();
            UpdateSnapshotNavigation();
            
            // Populate session selector
            RefreshSessionSelector();
        }
        
        private void InitializeTree()
        {
            if (BufferTree == null) return;
            
            BufferTree.Clear();
            BufferTree.Columns = 2;
            BufferTree.SetColumnTitle(0, "Name");
            BufferTree.SetColumnTitle(1, "Value");
            BufferTree.ColumnTitlesVisible = true;
            BufferTree.HideRoot = false;
            
            _rootItem = BufferTree.CreateItem();
            _rootItem.SetText(0, "Buffers");
            _rootItem.SetText(1, "");
        }
        
        // Made by AI: Claude Opus 4.5 - Take snapshot button handler
        public void OnSnapshotPressed()
        {
            // If viewing old session, switch back to current
            if (_isViewingOldSession)
            {
                _loadedSessionId = DebugDataManager.CurrentSessionId;
                _loadedSnapshots.Clear();
                _loadedSnapshots.AddRange(DebugDataManager.CurrentSessionSnapshots);
                _isViewingOldSession = false;
                UpdateWarningVisibility();
            }
            
            var info = DebugDataManager.TakeSnapshot();
            _loadedSnapshots.Clear();
            _loadedSnapshots.AddRange(DebugDataManager.CurrentSessionSnapshots);
            _currentSnapshotIndex = _loadedSnapshots.Count - 1;
            
            DisplayCurrentSnapshot();
            UpdateSnapshotNavigation();
        }
        
        // Made by AI: Claude Opus 4.5 - Navigate to previous snapshot
        public void OnPrevSnapshotPressed()
        {
            if (_currentSnapshotIndex > 0)
            {
                _currentSnapshotIndex--;
                DisplayCurrentSnapshot();
                UpdateSnapshotNavigation();
            }
        }
        
        // Made by AI: Claude Opus 4.5 - Navigate to next snapshot
        public void OnNextSnapshotPressed()
        {
            if (_currentSnapshotIndex < _loadedSnapshots.Count - 1)
            {
                _currentSnapshotIndex++;
                DisplayCurrentSnapshot();
                UpdateSnapshotNavigation();
            }
        }
        
        // Made by AI: Claude Opus 4.5 - Load session button handler
        public void OnLoadSessionPressed()
        {
            if (SessionSelector == null) return;
            
            int selected = SessionSelector.Selected;
            if (selected < 0) return;
            
            string sessionId = SessionSelector.GetItemText(selected);
            LoadSession(sessionId);
        }
        
        // Made by AI: Claude Opus 4.5 - Load a specific session
        private void LoadSession(string sessionId)
        {
            _loadedSnapshots = DebugDataManager.LoadSessionSnapshots(sessionId);
            _loadedSessionId = sessionId;
            _isViewingOldSession = sessionId != DebugDataManager.CurrentSessionId;
            
            if (_loadedSnapshots.Count > 0)
            {
                _currentSnapshotIndex = _loadedSnapshots.Count - 1; // Show latest snapshot
                DisplayCurrentSnapshot();
            }
            else
            {
                _currentSnapshotIndex = -1;
                ClearTree();
            }
            
            UpdateWarningVisibility();
            UpdateSnapshotNavigation();
        }
        
        // Made by AI: Claude Opus 4.5 - Display the current snapshot data
        private void DisplayCurrentSnapshot()
        {
            if (_currentSnapshotIndex < 0 || _currentSnapshotIndex >= _loadedSnapshots.Count)
            {
                ClearTree();
                return;
            }
            
            var snapshotInfo = _loadedSnapshots[_currentSnapshotIndex];
            var data = DebugDataManager.LoadSnapshot(snapshotInfo.FilePath);
            
            // Made by AI: Claude Opus 4.5 - Changed null check for class type
            if (data != null)
            {
                PopulateTreeFromSnapshot(data);
            }
        }
        
        // Made by AI: Claude Opus 4.5 - Clear tree content
        private void ClearTree()
        {
            if (BufferTree == null) return;
            
            // Remove children but keep root
            if (_bonusGiverRoot != null)
            {
                _rootItem.RemoveChild(_bonusGiverRoot);
                _bonusGiverRoot = null;
            }
            if (_bonusStepRoot != null)
            {
                _rootItem.RemoveChild(_bonusStepRoot);
                _bonusStepRoot = null;
            }
        }
        
        // Made by AI: Claude Opus 4.5 - Populate tree from snapshot data (changed parameter type from struct to class)
        private void PopulateTreeFromSnapshot(DebugDataManager.SnapshotData data)
        {
            if (_isRefreshing || BufferTree == null) return;
            _isRefreshing = true;
            
            try
            {
                ClearTree();
                
                // Populate BonusGiverBuffer data
                PopulateBonusGiverBuffer(data.BonusGiverData);
                
                // Populate BonusStepBuffer data
                PopulateBonusStepBuffer(data.BonusStepData);
            }
            finally
            {
                _isRefreshing = false;
            }
        }
        
        private void PopulateBonusGiverBuffer(Logic.DataBuffer.BonusGiverBufferDebug.BonusGiverBufferDebugData debugData)
        {
            // Made by AI: Claude Opus 4.5 - Added null check for debugData
            if (debugData == null)
            {
                _bonusGiverRoot = BufferTree.CreateItem(_rootItem);
                _bonusGiverRoot.SetText(0, "BonusGiverBuffer");
                _bonusGiverRoot.SetText(1, "(no data)");
                return;
            }
            
            _bonusGiverRoot = BufferTree.CreateItem(_rootItem);
            _bonusGiverRoot.SetText(0, "BonusGiverBuffer");
            _bonusGiverRoot.SetText(1, $"Lists: {debugData.ListsCount}, Used: {debugData.UsedCapacity}");
            
            // Add buffer info
            var infoItem = BufferTree.CreateItem(_bonusGiverRoot);
            infoItem.SetText(0, "Buffer Info");
            infoItem.SetText(1, "");
            
            AddInfoItem(infoItem, "Buffer Length", debugData.BufferLength.ToString());
            AddInfoItem(infoItem, "Lists Count", debugData.ListsCount.ToString());
            AddInfoItem(infoItem, "Used Capacity", debugData.UsedCapacity.ToString());
            
            // Add lists - Made by AI: Claude Opus 4.5 - Added null check for Lists
            var listsItem = BufferTree.CreateItem(_bonusGiverRoot);
            listsItem.SetText(0, "Lists");
            listsItem.SetText(1, debugData.ListsCount.ToString());
            
            if (debugData.Lists == null || debugData.Lists.Length == 0)
                return;
            
            for (int i = 0; i < debugData.Lists.Length; i++)
            {
                var list = debugData.Lists[i];
                if (list == null) continue;
                
                var listItem = BufferTree.CreateItem(listsItem);
                listItem.SetText(0, $"List[{i}]");
                listItem.SetText(1, $"Count: {list.Count}, Start: {list.Start}, Cap: {list.Capacity}");
                
                // Add items in this list - Made by AI: Claude Opus 4.5 - Added null check for Items
                if (list.Items == null) continue;
                
                for (int j = 0; j < list.Count && j < list.Items.Length; j++)
                {
                    var item = list.Items[j];
                    if (item == null) continue;
                    
                    var itemNode = BufferTree.CreateItem(listItem);
                    itemNode.SetText(0, $"BonusGiver[{j}]");
                    itemNode.SetText(1, $"Tile: {item.MapTileId}, Effect: {item.EffectIdx}");
                }
            }
        }
        
        private void PopulateBonusStepBuffer(Logic.DataBuffer.BonusStepBufferDebug.BonusStepBufferDebugData debugData)
        {
            // Made by AI: Claude Opus 4.5 - Added null check for debugData
            if (debugData == null)
            {
                _bonusStepRoot = BufferTree.CreateItem(_rootItem);
                _bonusStepRoot.SetText(0, "BonusStepBuffer");
                _bonusStepRoot.SetText(1, "(no data)");
                return;
            }
            
            _bonusStepRoot = BufferTree.CreateItem(_rootItem);
            _bonusStepRoot.SetText(0, "BonusStepBuffer");
            _bonusStepRoot.SetText(1, $"Lists: {debugData.ListsCount}, Used: {debugData.UsedCapacity}");
            
            // Add buffer info
            var infoItem = BufferTree.CreateItem(_bonusStepRoot);
            infoItem.SetText(0, "Buffer Info");
            infoItem.SetText(1, "");
            
            AddInfoItem(infoItem, "Buffer Length", debugData.BufferLength.ToString());
            AddInfoItem(infoItem, "Lists Count", debugData.ListsCount.ToString());
            AddInfoItem(infoItem, "Used Capacity", debugData.UsedCapacity.ToString());
            
            // Add lists - Made by AI: Claude Opus 4.5 - Added null check for Lists
            var listsItem = BufferTree.CreateItem(_bonusStepRoot);
            listsItem.SetText(0, "Lists");
            listsItem.SetText(1, debugData.ListsCount.ToString());
            
            if (debugData.Lists == null || debugData.Lists.Length == 0)
                return;
            
            for (int i = 0; i < debugData.Lists.Length; i++)
            {
                var list = debugData.Lists[i];
                if (list == null) continue;
                
                var listItem = BufferTree.CreateItem(listsItem);
                listItem.SetText(0, $"List[{i}]");
                listItem.SetText(1, $"Count: {list.Count}, Start: {list.Start}, Cap: {list.Capacity}");
                
                // Add items in this list - Made by AI: Claude Opus 4.5 - Added null check for Items
                if (list.Items == null) continue;
                
                for (int j = 0; j < list.Count && j < list.Items.Length; j++)
                {
                    var item = list.Items[j];
                    if (item == null) continue;
                    
                    var itemNode = BufferTree.CreateItem(listItem);
                    itemNode.SetText(0, $"BonusStep[{j}]");
                    itemNode.SetText(1, $"Tile: {item.TileId}, Src: {item.SourceTileId}, D: {item.Depth}, Type: {item.BonusType}, Val: {item.Value}");
                }
            }
        }
        
        private void AddInfoItem(TreeItem parent, string name, string value)
        {
            var item = BufferTree.CreateItem(parent);
            item.SetText(0, name);
            item.SetText(1, value);
        }
        
        // Made by AI: Claude Opus 4.5 - Update warning panel visibility
        private void UpdateWarningVisibility()
        {
            if (WarningPanel != null)
            {
                WarningPanel.Visible = _isViewingOldSession;
            }
            if (WarningLabel != null && _isViewingOldSession)
            {
                WarningLabel.Text = $"Viewing data from session: {_loadedSessionId}";
            }
        }
        
        // Made by AI: Claude Opus 4.5 - Update snapshot navigation UI
        private void UpdateSnapshotNavigation()
        {
            if (PrevSnapshotButton != null)
            {
                PrevSnapshotButton.Disabled = _currentSnapshotIndex <= 0;
            }
            
            if (NextSnapshotButton != null)
            {
                NextSnapshotButton.Disabled = _currentSnapshotIndex >= _loadedSnapshots.Count - 1;
            }
            
            if (SnapshotLabel != null)
            {
                if (_loadedSnapshots.Count == 0)
                {
                    SnapshotLabel.Text = "No snapshots";
                }
                else
                {
                    SnapshotLabel.Text = $"{_currentSnapshotIndex + 1} / {_loadedSnapshots.Count}";
                }
            }
        }
        
        // Made by AI: Claude Opus 4.5 - Refresh session selector dropdown
        private void RefreshSessionSelector()
        {
            if (SessionSelector == null) return;
            
            SessionSelector.Clear();
            var sessions = DebugDataManager.GetAvailableSessions();
            
            foreach (var session in sessions)
            {
                string label = session;
                if (session == DebugDataManager.CurrentSessionId)
                {
                    label += " (current)";
                }
                SessionSelector.AddItem(label);
            }
            
            // Select current session
            for (int i = 0; i < SessionSelector.ItemCount; i++)
            {
                if (SessionSelector.GetItemText(i).StartsWith(DebugDataManager.CurrentSessionId))
                {
                    SessionSelector.Selected = i;
                    break;
                }
            }
        }
        
        private void OnCloseRequested()
        {
            Hide();
        }
        
        public override void _UnhandledKeyInput(InputEvent @event)
        {
            if (@event is InputEventKey keyEvent && keyEvent.Pressed && !keyEvent.Echo)
            {
                if (keyEvent.Keycode == Key.F2)
                {
                    OnSnapshotPressed();
                    GetViewport().SetInputAsHandled();
                }
            }
        }
        
        public void Toggle()
        {
            if (Visible)
            {
                Hide();
            }
            else
            {
                RefreshSessionSelector();
                Show();
                MoveToCenter();
            }
        }
        
        private void MoveToCenter()
        {
            var screenSize = DisplayServer.WindowGetSize();
            var windowSize = Size;
            Position = new Vector2I(
                (screenSize.X - windowSize.X) / 2,
                (screenSize.Y - windowSize.Y) / 2
            );
        }
    }
}
