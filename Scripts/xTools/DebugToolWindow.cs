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
        
        // Deck Inspector exports
        [Export]
        private Tree DeckTree;
        
        [Export]
        private Button DeckPrevTurnButton;
        
        [Export]
        private Button DeckNextTurnButton;
        
        [Export]
        private Label DeckTurnLabel;
        
        [Export]
        private Button DeckRefreshButton;
        
        [Export]
        private Button DeckExpandAllButton;
        
        [Export]
        private Button DeckCollapseAllButton;
        
        // Map Inspector exports
        [Export]
        private Tree MapTree;
        
        [Export]
        private Button MapPrevTurnButton;
        
        [Export]
        private Button MapNextTurnButton;
        
        [Export]
        private Label MapTurnLabel;
        
        [Export]
        private Button MapRefreshButton;
        
        [Export]
        private Button MapExpandAllButton;
        
        [Export]
        private Button MapCollapseAllButton;
        
        // Buffer Inspector exports
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
        
        // Buffer Inspector tree items
        private TreeItem _bufferRootItem;
        private TreeItem _bonusGiverRoot;
        private TreeItem _bonusStepRoot;
        
        // Deck Inspector tree items
        private TreeItem _deckRootItem;
        
        // Map Inspector tree items
        private TreeItem _mapRootItem;
        
        private bool _isRefreshing = false;
        
        // Deck Inspector state
        private int _currentDeckTurn = 0;
        
        // Map Inspector state
        private int _currentMapTurn = 0;
        
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
            
            // Set up tabs - disable future feature tabs (indices 3-4)
            if (TabContainer != null)
            {
                TabContainer.SetTabTitle(0, "Deck Inspector");
                TabContainer.SetTabTitle(1, "Map Inspector");
                TabContainer.SetTabTitle(2, "Buffer Inspector");
                for (int i = 3; i < TabContainer.GetTabCount(); i++)
                {
                    TabContainer.SetTabDisabled(i, true);
                    TabContainer.SetTabTitle(i, $"Future Feature {i - 2}");
                }
            }
            
            // Initialize trees
            InitializeDeckTree();
            InitializeMapTree();
            InitializeBufferTree();
            
            // Made by AI: Claude Opus 4.5 - Initialize snapshot system
            _loadedSessionId = DebugDataManager.CurrentSessionId;
            _isViewingOldSession = false;
            UpdateWarningVisibility();
            UpdateSnapshotNavigation();
            
            // Populate session selector
            RefreshSessionSelector();
        }
        
        // ===== DECK INSPECTOR =====
        
        private void InitializeDeckTree()
        {
            if (DeckTree == null) return;
            
            DeckTree.Clear();
            DeckTree.Columns = 2;
            DeckTree.SetColumnTitle(0, "Index / Property");
            DeckTree.SetColumnTitle(1, "Value");
            DeckTree.ColumnTitlesVisible = true;
            DeckTree.HideRoot = false;
            
            _deckRootItem = DeckTree.CreateItem();
            _deckRootItem.SetText(0, "DeckTiles");
            _deckRootItem.SetText(1, "");
        }
        
        private void RefreshDeckInspector()
        {
            if (_isRefreshing || DeckTree == null) return;
            _isRefreshing = true;
            
            try
            {
                // Get current turn from game data
                int currentTurn = Hex.Data.Game.GetTurnCount();
                
                // Default to current turn when opening
                _currentDeckTurn = currentTurn;
                
                UpdateDeckTurnNavigation();
                PopulateDeckTiles();
            }
            finally
            {
                _isRefreshing = false;
            }
        }
        
        public void OnDeckPrevTurnPressed()
        {
            if (_currentDeckTurn > 0)
            {
                _currentDeckTurn--;
                UpdateDeckTurnNavigation();
                PopulateDeckTiles();
            }
        }
        
        public void OnDeckNextTurnPressed()
        {
            int currentTurn = Hex.Data.Game.GetTurnCount();
            if (_currentDeckTurn < currentTurn)
            {
                _currentDeckTurn++;
                UpdateDeckTurnNavigation();
                PopulateDeckTiles();
            }
        }
        
        public void OnRefreshDeckPressed()
        {
            // Refresh to current turn
            int currentTurn = Hex.Data.Game.GetTurnCount();
            _currentDeckTurn = currentTurn;
            UpdateDeckTurnNavigation();
            PopulateDeckTiles();
        }
        
        public void OnDeckExpandAllPressed()
        {
            if (_deckRootItem == null) return;
            SetTreeItemsCollapsed(_deckRootItem, false);
        }
        
        public void OnDeckCollapseAllPressed()
        {
            if (_deckRootItem == null) return;
            SetTreeItemsCollapsed(_deckRootItem, true);
        }
        
        private void UpdateDeckTurnNavigation()
        {
            int currentTurn = Hex.Data.Game.GetTurnCount();
            
            if (DeckPrevTurnButton != null)
            {
                DeckPrevTurnButton.Disabled = _currentDeckTurn <= 0;
            }
            
            if (DeckNextTurnButton != null)
            {
                DeckNextTurnButton.Disabled = _currentDeckTurn >= currentTurn;
            }
            
            if (DeckTurnLabel != null)
            {
                DeckTurnLabel.Text = $"{_currentDeckTurn} / {currentTurn}";
            }
        }
        
        private void PopulateDeckTiles()
        {
            if (DeckTree == null || _deckRootItem == null) return;
            
            // Clear existing children
            var child = _deckRootItem.GetFirstChild();
            while (child != null)
            {
                var next = child.GetNext();
                _deckRootItem.RemoveChild(child);
                child = next;
            }
            
            try
            {
                // Get deck tiles for the selected turn
                var deckTiles = Hex.Data.Game.DeckTiles.GetTileCollectionFromHistory(_currentDeckTurn);
                int deckTileCount = Hex.Data.Game.DeckTiles.GetDeckTileCount(_currentDeckTurn);
                
                _deckRootItem.SetText(0, "DeckTiles");
                _deckRootItem.SetText(1, $"Turn {_currentDeckTurn}, Count: {deckTileCount}");
                
                for (int i = 0; i < deckTiles.Length; i++)
                {
                    ref var tile = ref deckTiles[i];
                    
                    var tileItem = DeckTree.CreateItem(_deckRootItem);
                    
                    // Get tile name from Def if valid
                    string tileName = tile.IsValid() ? tile.Def.Name : "(empty)";
                    string stateStr = tile.IsValid() ? tile.State.ToString() : "";
                    tileItem.SetText(0, $"[{i}] {tileName}");
                    tileItem.SetText(1, tile.IsValid() ? $"State: {stateStr}" : "");
                    
                    if (tile.IsValid())
                    {
                        // Add State
                        AddDeckTileInfoItem(tileItem, "State", tile.State.ToString());
                        
                        // Add DefData details
                        var defDataItem = DeckTree.CreateItem(tileItem);
                        defDataItem.SetText(0, "DefData");
                        defDataItem.SetText(1, "");
                        
                        AddDeckTileInfoItem(defDataItem, "Starting", tile.DefData.Starting.ToString());
                        AddDeckTileInfoItem(defDataItem, "Level", tile.DefData.Level.ToString());
                        AddDeckTileInfoItem(defDataItem, "Weight", tile.DefData.Weight.ToString());
                        AddDeckTileInfoItem(defDataItem, "Initiative", tile.DefData.Initiative.ToString());
                        
                        // Terrain Tags
                        if (tile.DefData.TerrainTags.Count > 0)
                        {
                            var terrainItem = DeckTree.CreateItem(defDataItem);
                            terrainItem.SetText(0, "TerrainTags");
                            terrainItem.SetText(1, $"Count: {tile.DefData.TerrainTags.Count}");
                            
                            for (int t = 0; t < tile.DefData.TerrainTags.Count; t++)
                            {
                                var tag = tile.DefData.TerrainTags[t];
                                AddDeckTileInfoItem(terrainItem, $"[{t}]", tag.Name);
                            }
                        }
                        
                        // Building Tags
                        if (tile.DefData.BuildingTags.Count > 0)
                        {
                            var buildingItem = DeckTree.CreateItem(defDataItem);
                            buildingItem.SetText(0, "BuildingTags");
                            buildingItem.SetText(1, $"Count: {tile.DefData.BuildingTags.Count}");
                            
                            for (int b = 0; b < tile.DefData.BuildingTags.Count; b++)
                            {
                                var tag = tile.DefData.BuildingTags[b];
                                AddDeckTileInfoItem(buildingItem, $"[{b}]", tag.Name);
                            }
                        }
                        
                        // Conditions
                        if (tile.DefData.Conditions.Count > 0)
                        {
                            var conditionsItem = DeckTree.CreateItem(defDataItem);
                            conditionsItem.SetText(0, "Conditions");
                            conditionsItem.SetText(1, $"Count: {tile.DefData.Conditions.Count}");
                            
                            for (int c = 0; c < tile.DefData.Conditions.Count; c++)
                            {
                                ref var cond = ref tile.DefData.Conditions[c];
                                AddDeckTileInfoItem(conditionsItem, $"[{c}]", cond.ToString());
                            }
                        }
                        
                        // Effects
                        if (tile.DefData.Effects.Count > 0)
                        {
                            var effectsItem = DeckTree.CreateItem(defDataItem);
                            effectsItem.SetText(0, "Effects");
                            effectsItem.SetText(1, $"Count: {tile.DefData.Effects.Count}");
                            
                            for (int e = 0; e < tile.DefData.Effects.Count; e++)
                            {
                                ref var effect = ref tile.DefData.Effects[e];
                                AddDeckTileInfoItem(effectsItem, $"[{e}]", effect.ToString());
                            }
                        }
                        
                        // Collapse defDataItem by default
                        defDataItem.Collapsed = true;
                    }
                    
                    // Collapse tileItem by default
                    tileItem.Collapsed = true;
                }
            }
            catch (Exception ex)
            {
                _deckRootItem.SetText(1, $"Error: {ex.Message}");
            }
        }
        
        private void AddDeckTileInfoItem(TreeItem parent, string name, string value)
        {
            var item = DeckTree.CreateItem(parent);
            item.SetText(0, name);
            item.SetText(1, value);
        }
        
        // ===== MAP INSPECTOR =====
        
        private void InitializeMapTree()
        {
            if (MapTree == null) return;
            
            MapTree.Clear();
            MapTree.Columns = 2;
            MapTree.SetColumnTitle(0, "Index / Property");
            MapTree.SetColumnTitle(1, "Value");
            MapTree.ColumnTitlesVisible = true;
            MapTree.HideRoot = false;
            
            _mapRootItem = MapTree.CreateItem();
            _mapRootItem.SetText(0, "MapTiles");
            _mapRootItem.SetText(1, "");
        }
        
        private void RefreshMapInspector()
        {
            if (_isRefreshing || MapTree == null) return;
            _isRefreshing = true;
            
            try
            {
                // Get current turn from game data
                int currentTurn = Hex.Data.Game.GetTurnCount();
                
                // Default to current turn when opening
                _currentMapTurn = currentTurn;
                
                UpdateMapTurnNavigation();
                PopulateMapTiles();
            }
            finally
            {
                _isRefreshing = false;
            }
        }
        
        public void OnMapPrevTurnPressed()
        {
            if (_currentMapTurn > 0)
            {
                _currentMapTurn--;
                UpdateMapTurnNavigation();
                PopulateMapTiles();
            }
        }
        
        public void OnMapNextTurnPressed()
        {
            int currentTurn = Hex.Data.Game.GetTurnCount();
            if (_currentMapTurn < currentTurn)
            {
                _currentMapTurn++;
                UpdateMapTurnNavigation();
                PopulateMapTiles();
            }
        }
        
        public void OnRefreshMapPressed()
        {
            // Refresh to current turn
            int currentTurn = Hex.Data.Game.GetTurnCount();
            _currentMapTurn = currentTurn;
            UpdateMapTurnNavigation();
            PopulateMapTiles();
        }
        
        public void OnMapExpandAllPressed()
        {
            if (_mapRootItem == null) return;
            SetTreeItemsCollapsed(_mapRootItem, false);
        }
        
        public void OnMapCollapseAllPressed()
        {
            if (_mapRootItem == null) return;
            SetTreeItemsCollapsed(_mapRootItem, true);
        }
        
        private void SetTreeItemsCollapsed(TreeItem item, bool collapsed)
        {
            item.Collapsed = collapsed;
            var child = item.GetFirstChild();
            while (child != null)
            {
                SetTreeItemsCollapsed(child, collapsed);
                child = child.GetNext();
            }
        }
        
        private void UpdateMapTurnNavigation()
        {
            int currentTurn = Hex.Data.Game.GetTurnCount();
            
            if (MapPrevTurnButton != null)
            {
                MapPrevTurnButton.Disabled = _currentMapTurn <= 0;
            }
            
            if (MapNextTurnButton != null)
            {
                MapNextTurnButton.Disabled = _currentMapTurn >= currentTurn;
            }
            
            if (MapTurnLabel != null)
            {
                MapTurnLabel.Text = $"{_currentMapTurn} / {currentTurn}";
            }
        }
        
        private void PopulateMapTiles()
        {
            if (MapTree == null || _mapRootItem == null) return;
            
            // Clear existing children
            var child = _mapRootItem.GetFirstChild();
            while (child != null)
            {
                var next = child.GetNext();
                _mapRootItem.RemoveChild(child);
                child = next;
            }
            
            try
            {
                // Get map tiles for the selected turn
                var mapTiles = Hex.Data.Game.MapTiles.GetTileCollectionFromHistory(_currentMapTurn);
                
                _mapRootItem.SetText(0, "MapTiles");
                _mapRootItem.SetText(1, $"Turn {_currentMapTurn}, Count: {Hex.Data.MapTile.MAP_SIZE}");
                
                for (int i = 0; i < mapTiles.Length; i++)
                {
                    ref var tile = ref mapTiles[i];
                    
                    var tileItem = MapTree.CreateItem(_mapRootItem);
                    
                    // Get tile name from Def if valid
                    string tileName = tile.IsValid() ? tile.Def.Name : "(empty)";
                    tileItem.SetText(0, $"[{i}] {tileName}");
                    tileItem.SetText(1, tile.IsValid() ? $"Level: {tile.DefData.Level}" : "");
                    
                    if (tile.IsValid())
                    {
                        // Add DefData details
                        var defDataItem = MapTree.CreateItem(tileItem);
                        defDataItem.SetText(0, "DefData");
                        defDataItem.SetText(1, "");
                        
                        AddMapTileInfoItem(defDataItem, "Starting", tile.DefData.Starting.ToString());
                        AddMapTileInfoItem(defDataItem, "Level", tile.DefData.Level.ToString());
                        AddMapTileInfoItem(defDataItem, "Weight", tile.DefData.Weight.ToString());
                        AddMapTileInfoItem(defDataItem, "Initiative", tile.DefData.Initiative.ToString());
                        
                        // Terrain Tags
                        if (tile.DefData.TerrainTags.Count > 0)
                        {
                            var terrainItem = MapTree.CreateItem(defDataItem);
                            terrainItem.SetText(0, "TerrainTags");
                            terrainItem.SetText(1, $"Count: {tile.DefData.TerrainTags.Count}");
                            
                            for (int t = 0; t < tile.DefData.TerrainTags.Count; t++)
                            {
                                var tag = tile.DefData.TerrainTags[t];
                                AddMapTileInfoItem(terrainItem, $"[{t}]", tag.Name);
                            }
                        }
                        
                        // Building Tags
                        if (tile.DefData.BuildingTags.Count > 0)
                        {
                            var buildingItem = MapTree.CreateItem(defDataItem);
                            buildingItem.SetText(0, "BuildingTags");
                            buildingItem.SetText(1, $"Count: {tile.DefData.BuildingTags.Count}");
                            
                            for (int b = 0; b < tile.DefData.BuildingTags.Count; b++)
                            {
                                var tag = tile.DefData.BuildingTags[b];
                                AddMapTileInfoItem(buildingItem, $"[{b}]", tag.Name);
                            }
                        }
                        
                        // Conditions
                        if (tile.DefData.Conditions.Count > 0)
                        {
                            var conditionsItem = MapTree.CreateItem(defDataItem);
                            conditionsItem.SetText(0, "Conditions");
                            conditionsItem.SetText(1, $"Count: {tile.DefData.Conditions.Count}");
                            
                            for (int c = 0; c < tile.DefData.Conditions.Count; c++)
                            {
                                ref var cond = ref tile.DefData.Conditions[c];
                                AddMapTileInfoItem(conditionsItem, $"[{c}]", cond.ToString());
                            }
                        }
                        
                        // Effects
                        if (tile.DefData.Effects.Count > 0)
                        {
                            var effectsItem = MapTree.CreateItem(defDataItem);
                            effectsItem.SetText(0, "Effects");
                            effectsItem.SetText(1, $"Count: {tile.DefData.Effects.Count}");
                            
                            for (int e = 0; e < tile.DefData.Effects.Count; e++)
                            {
                                ref var effect = ref tile.DefData.Effects[e];
                                AddMapTileInfoItem(effectsItem, $"[{e}]", effect.ToString());
                            }
                        }
                        
                        // Collapse defDataItem by default
                        defDataItem.Collapsed = true;
                    }
                    
                    // Collapse tileItem by default
                    tileItem.Collapsed = true;
                }
            }
            catch (Exception ex)
            {
                _mapRootItem.SetText(1, $"Error: {ex.Message}");
            }
        }
        
        private void AddMapTileInfoItem(TreeItem parent, string name, string value)
        {
            var item = MapTree.CreateItem(parent);
            item.SetText(0, name);
            item.SetText(1, value);
        }
        
        // ===== BUFFER INSPECTOR =====
        
        private void InitializeBufferTree()
        {
            if (BufferTree == null) return;
            
            BufferTree.Clear();
            BufferTree.Columns = 2;
            BufferTree.SetColumnTitle(0, "Name");
            BufferTree.SetColumnTitle(1, "Value");
            BufferTree.ColumnTitlesVisible = true;
            BufferTree.HideRoot = false;
            
            _bufferRootItem = BufferTree.CreateItem();
            _bufferRootItem.SetText(0, "Buffers");
            _bufferRootItem.SetText(1, "");
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
                ClearBufferTree();
            }
            
            UpdateWarningVisibility();
            UpdateSnapshotNavigation();
        }
        
        // Made by AI: Claude Opus 4.5 - Display the current snapshot data
        private void DisplayCurrentSnapshot()
        {
            if (_currentSnapshotIndex < 0 || _currentSnapshotIndex >= _loadedSnapshots.Count)
            {
                ClearBufferTree();
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
        private void ClearBufferTree()
        {
            if (BufferTree == null) return;
            
            // Remove children but keep root
            if (_bonusGiverRoot != null)
            {
                _bufferRootItem.RemoveChild(_bonusGiverRoot);
                _bonusGiverRoot = null;
            }
            if (_bonusStepRoot != null)
            {
                _bufferRootItem.RemoveChild(_bonusStepRoot);
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
                ClearBufferTree();
                
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
                _bonusGiverRoot = BufferTree.CreateItem(_bufferRootItem);
                _bonusGiverRoot.SetText(0, "BonusGiverBuffer");
                _bonusGiverRoot.SetText(1, "(no data)");
                return;
            }
            
            _bonusGiverRoot = BufferTree.CreateItem(_bufferRootItem);
            _bonusGiverRoot.SetText(0, "BonusGiverBuffer");
            _bonusGiverRoot.SetText(1, $"Lists: {debugData.ListsCount}, Used: {debugData.UsedCapacity}");
            
            // Add buffer info
            var infoItem = BufferTree.CreateItem(_bonusGiverRoot);
            infoItem.SetText(0, "Buffer Info");
            infoItem.SetText(1, "");
            
            AddBufferInfoItem(infoItem, "Buffer Length", debugData.BufferLength.ToString());
            AddBufferInfoItem(infoItem, "Lists Count", debugData.ListsCount.ToString());
            AddBufferInfoItem(infoItem, "Used Capacity", debugData.UsedCapacity.ToString());
            
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
                _bonusStepRoot = BufferTree.CreateItem(_bufferRootItem);
                _bonusStepRoot.SetText(0, "BonusStepBuffer");
                _bonusStepRoot.SetText(1, "(no data)");
                return;
            }
            
            _bonusStepRoot = BufferTree.CreateItem(_bufferRootItem);
            _bonusStepRoot.SetText(0, "BonusStepBuffer");
            _bonusStepRoot.SetText(1, $"Lists: {debugData.ListsCount}, Used: {debugData.UsedCapacity}");
            
            // Add buffer info
            var infoItem = BufferTree.CreateItem(_bonusStepRoot);
            infoItem.SetText(0, "Buffer Info");
            infoItem.SetText(1, "");
            
            AddBufferInfoItem(infoItem, "Buffer Length", debugData.BufferLength.ToString());
            AddBufferInfoItem(infoItem, "Lists Count", debugData.ListsCount.ToString());
            AddBufferInfoItem(infoItem, "Used Capacity", debugData.UsedCapacity.ToString());
            
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
        
        private void AddBufferInfoItem(TreeItem parent, string name, string value)
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
                RefreshDeckInspector();
                RefreshMapInspector();
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
