using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Linq;

public class Spawner : MonoBehaviour {
    static public float HellSpawnSpeedDefault = 1.0f;
    static public float HellSpawnSpeedUp = 2.0f;
    static public string SPAWN_CHAIN_SEQUENCE = "SPAWNER_SPAWN_CHAIN_SEQUENCE";
    static public string SPAWN_GROUP_SEQUENCE = "SPAWNER_GROUP_SEQUENCE";
    static public bool IsSpawnerSpeedup = false;

    [System.Serializable]
    public struct SpawnItemType
    {
        public string name;
        public bool isChain;        

        public SpawnItemType(string pName, bool pIsChain = false)
        {
            name = pName;
            isChain = pIsChain;
        }        
    }

    /**
    * Structure for current spawn chain information
    */
    public struct CurrentSpawnChain
    {
        public int amountLeft;
        public int amountTotal;
        public SpawnItemType spawnItemType;
        public GameObject spawnItemGameObject;
        public GameObject spawnItemUpGameObject;
        public GameObject spawnItemDownGameObject;
        public HellSpawn.SpawnMovingParameters spawnMovingParameters;
        public List<string> spawnOrder;
    }

    public GameObject[] spawnPositionGameObjects;

    public PlayerBonusStatus playerBonusStatus;

    public UnityEngine.UI.Text gameLevelText;
    public UnityEngine.UI.Text wavesText;

    public PlayerScore playerScore;

    private float _spawnTimeStart = 4.0f;
    private float _spawnWaveLag = 0.25f;
    private float _spawnTime;
    private float _spawnTimeScale = 1.0f;
    
    private float _spawnTimeSpeedupStep = 0.05f;
    private float _spawnTimeStopScale = 3.0f;

    private float _spawnFlyTimeStart = 10.0f;
    private float _spawnFlySpeed = 2.2f;
    private float _spawnFlyTime;
    private float _spawnFlyTimeScale = 1.0f;
    private float _spawnFlyTimeStep = 0.1f;
    private float _spawnFlyTimeStopScale = 5.0f;

    private Sequence _speedupSpawnerSequence;

    private bool _isPaused = false;

    private float _spawnChainTime = 0.25f;

    private int _spawnChainId = -1;
    private int[] _spawnChainAmount = new int[20];
    private int[] _spawnChainCount = new int[20];
    private bool _isPreviousChainFull = false;

    private float _slowDownTimeBonus = 10.0f;    

    private Sequence _spawnNextSequence;    
    private Sequence _slowDownHellSpawnsSequence;       

    private static Dictionary<GameObject, bool> _spawnedObjects = new Dictionary<GameObject, bool>();
        
    private Dictionary<string, List<SpawnItemType>> _spawnItemGroup = new Dictionary<string, List<SpawnItemType>>();    
    private Dictionary<string, int> _spawnItemGroupWeights = new Dictionary<string, int>();

    private float _globalBlockWeight = 100.0f;
    private float _overallWeight = 1000.0f;
        
    private List<GameObject> _previousSpawnPositionGameObjects = new List<GameObject>();

    private int _wavesSpeedupStart = 10;
    private int _wavesSpeedupStep = 5;
    private int _currentWavesAmount = 0;

    private int gameLevel = 1;
    private int _nextLevelWavesAmount = 0;

    private string updateGameLevelTextTemplate = "Game level: {0}";
    private string updateWavesTextTemplate = "Waves: {0}/{1}";
    private int updateGameLevelTextMax = 9999999;
    private int updateWavesTextMax = 999999;

    private Dictionary<string, int> _spawnItemGroupNext = new Dictionary<string, int>();
    
    void Awake()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        List<SpawnItemType> blockGroupItems = new List<SpawnItemType> {
            new SpawnItemType("blocker_01"),
            new SpawnItemType("blocker_02"),
            new SpawnItemType("blocker_03"),
            new SpawnItemType("blocker_04"),
            new SpawnItemType("blocker_05"),
            new SpawnItemType("blocker_06"),
        };
        List<SpawnItemType> globalBlockGroupItems = new List<SpawnItemType> {
            new SpawnItemType("lightning_wall_01"),
            new SpawnItemType("lightning_wall_02"),
            new SpawnItemType("lightning_wall_03"),
        };
        List<SpawnItemType> pickupGroupItems = new List<SpawnItemType> {            
            new SpawnItemType("star"),
            new SpawnItemType("speedup"),
            new SpawnItemType("bullet_bonus"),
            new SpawnItemType("shield"),
        };
        List<SpawnItemType> enemyGroupItems = new List<SpawnItemType> {
            new SpawnItemType("hellspawn_enemy_01"),
            new SpawnItemType("hellspawn_enemy_02"),
            new SpawnItemType("hellspawn_enemy_03"),
            new SpawnItemType("hellspawn_enemy_04"),
            new SpawnItemType("hellspawn_enemy_05"),
            new SpawnItemType("hellspawn_enemy_06"),
            new SpawnItemType("hellspawn_enemy_07"),
            new SpawnItemType("hellspawn_enemy_08"),
        };
        List<SpawnItemType> coinsGroupItems = new List<SpawnItemType> {
            new SpawnItemType("coin", true),
        };        
        
        //item types and groupd        
        _spawnItemGroup.Add("block", blockGroupItems);
        _spawnItemGroup.Add("global_block", globalBlockGroupItems);
        _spawnItemGroup.Add("pickup", pickupGroupItems);
        _spawnItemGroup.Add("enemy", enemyGroupItems);
        _spawnItemGroup.Add("coin", coinsGroupItems);
        
        //group drop weights
        _spawnItemGroupWeights.Add("block", 500);
        _spawnItemGroupWeights.Add("enemy", 500);
        _spawnItemGroupWeights.Add("coin", 500);
        _spawnItemGroupWeights.Add("pickup", 100);       
    }

    public void StartSpawner()
    {
        _currentWavesAmount = 0;
        gameLevel = 1;
        _spawnTime = _spawnTimeStart;
        _spawnFlyTime = _spawnFlyTimeStart;
        _spawnTimeScale = 1.0f;
        _spawnFlyTimeScale = 1.0f;

        _spawnNextSequence.Kill();
        DOTween.Kill(SPAWN_CHAIN_SEQUENCE);
        _slowDownHellSpawnsSequence.Kill();

        SequenceSpawnNext();
        UpdateTimeScales();
        UpdateGameLevelAndWavesText();       
    }

    private void UpdateGameLevelAndWavesText()
    {
        int _gameLevelForText = gameLevel;
        if (_gameLevelForText >= updateGameLevelTextMax) {
            _gameLevelForText = updateGameLevelTextMax;
        }
        int _wavesForText = _currentWavesAmount;
        if (_wavesForText >= updateWavesTextMax) {
            _wavesForText = updateWavesTextMax;
        }
        int _nextLevelWavesText = _nextLevelWavesAmount;
        if (_nextLevelWavesText >= updateWavesTextMax) {
            _nextLevelWavesText = updateWavesTextMax;
        }
        gameLevelText.text = string.Format(updateGameLevelTextTemplate, _gameLevelForText);
        wavesText.text = string.Format(updateWavesTextTemplate, _wavesForText, _nextLevelWavesText);
    }    

    private void SpawnerSpeedUp()
    {
        _spawnTimeScale += _spawnTimeSpeedupStep;
        _spawnFlyTimeScale += _spawnFlyTimeStep;
        if (_spawnTimeScale >= _spawnTimeStopScale) {
            _spawnTimeScale = _spawnTimeStopScale;
        }
        if (_spawnFlyTimeScale >= _spawnFlyTimeStopScale) {
            _spawnFlyTimeScale = _spawnFlyTimeStopScale;
        }
        UpdateTimeScales();               
    }    

    public void SpawnNext()
    {                
        //first, we must define if it's global block or not
        bool isGlobalBlock = RollGlobalBlock();

        if (isGlobalBlock) {
            SpawnGlobalBlock(); //spawn global block like lightning wall
        } else {
            SpawnGroupOfItems(); //spawn several items except global blocks
        }
        _currentWavesAmount++;        
        CheckLevelUp();
        UpdateGameLevelAndWavesText();
    }

    private void CheckLevelUp()
    {
        int _wavesForNextLevelNeeded = GetWavesForNextLevel();        
        if (_currentWavesAmount == _wavesForNextLevelNeeded) {
            GameLevelUp();
        }
        _nextLevelWavesAmount = GetWavesForNextLevel();
    }

    private int GetWavesForNextLevel()
    {
        int _wavesForNextLevelNeeded = 0;
        for (int i = 0; i < gameLevel; i++) {
            _wavesForNextLevelNeeded += _wavesSpeedupStart + i * _wavesSpeedupStep;
        }
        return _wavesForNextLevelNeeded;
    }
    

    private void GameLevelUp()
    {
        gameLevel++;
        playerScore.ScoreLevelUp();
        SpawnerSpeedUp();        
    }    

    private void SpawnGlobalBlock()
    {
        SpawnItemType spawnItemType = GetNextSpawnItemType("global_block");   
        List<GameObject> _tempSpawnPositions = spawnPositionGameObjects.ToList()
            .Except(_previousSpawnPositionGameObjects).ToList();      
        _previousSpawnPositionGameObjects.Clear();
        SpawnNormal(spawnItemType, _tempSpawnPositions);        
    }

    private List<bool> PrepareTempSpawnPositionIndexes(List<GameObject> spawnPositions, 
                                                       List<GameObject> _previousSpawnPositionGameObjects)
    {       
        List<bool> result = new List<bool>();
        for (int i = 0; i < spawnPositions.Count; i++) {
            result.Add(true);
        }

        foreach (GameObject previousPositionGameObject in _previousSpawnPositionGameObjects) {
            result[spawnPositions.IndexOf(previousPositionGameObject)] = false;
        }            
        
        return result;
    }

    private List<GameObject> RemoveUnwantedSpawnPositions(GameObject spawnPosition, 
                                                          List<GameObject> spawnPositions,
                                                          ref List<bool> _tempSpawnPositionIndexes)
    {
        List<GameObject> result = spawnPositions.ToList();        
        int spawnPositionIndex = spawnPositions.IndexOf(spawnPosition);
        int removeIndexStart = spawnPositionIndex - 2;
        int removeIndexRange = 5;
        if (removeIndexStart <= 0) {
            removeIndexStart = 0;                        
        }            
        if ((removeIndexStart + removeIndexRange) > spawnPositions.Count - 1) {
            removeIndexRange = spawnPositions.Count - removeIndexStart;            
        }
        for (int i = removeIndexStart; i < removeIndexStart + removeIndexRange; i++) {
            _tempSpawnPositionIndexes[i] = false;
        }        
        for (int i = _tempSpawnPositionIndexes.Count - 1; i >= 0 ; i--) {            
            if (!_tempSpawnPositionIndexes[i]) {                
                result.RemoveAt(i);                
            }
        }
        
        return result;               
    }

    private void SpawnGroupOfItems()
    {
        Sequence spawnGroupOfItemSequence = DOTween.Sequence();
        //first, lets define how many items will spawn
        int itemsAmount = GetRandomItemsAmount();                
        List<GameObject> _tempSpawnPositions = spawnPositionGameObjects.ToList();            
        List<bool> _tempSpawnPositionIndexes = PrepareTempSpawnPositionIndexes(_tempSpawnPositions, 
                                                                               _previousSpawnPositionGameObjects);                                                        
        _previousSpawnPositionGameObjects.Clear();
        Dictionary<string, int> spawnItemGroupWeights = _spawnItemGroupWeights.ToDictionary(entry => entry.Key, 
                                                                                            entry => entry.Value);
                                
        for (int i = 0; i < itemsAmount; i++) {
            spawnGroupOfItemSequence.AppendCallback(() =>
            {
                SpawnItemFromGroup(spawnItemGroupWeights, ref _tempSpawnPositions, ref _tempSpawnPositionIndexes);    
            });
            spawnGroupOfItemSequence.AppendInterval(_spawnWaveLag);
        }
        spawnGroupOfItemSequence.SetId(SPAWN_GROUP_SEQUENCE);
    }

    private List<GameObject> SpawnItemFromGroup(Dictionary<string, int> spawnItemGroupWeights, 
                                    ref List<GameObject> _tempSpawnPositions,
                                    ref List<bool> _tempSpawnPositionIndexes)
    {                          
        GameObject spawnPosition;
        string _spawnItemGroupName = RollRandomItemGroupName(spawnItemGroupWeights);
        spawnItemGroupWeights.Remove(_spawnItemGroupName);
        SpawnItemType spawnItemType = GetNextSpawnItemType(_spawnItemGroupName);      
        if (spawnItemType.isChain) {
            spawnPosition = SpawnChain(spawnItemType, _tempSpawnPositions);
        } else {
            spawnPosition = SpawnNormal(spawnItemType, _tempSpawnPositions);                
        }
        _previousSpawnPositionGameObjects.Add(spawnPosition);
        _tempSpawnPositions = RemoveUnwantedSpawnPositions(spawnPosition, 
            spawnPositionGameObjects.ToList(), 
            ref _tempSpawnPositionIndexes);
        return _tempSpawnPositions;
    }    

    private GameObject SpawnNormal(SpawnItemType spawnItemType, List<GameObject> spawnPositions = null)
    {        
        GameObject spawnPositionGameObject = GetRandomSpawnPositionGameObject(spawnPositions);
        GameObject spawnItemGameObject = GetSpawnItemGameObject(spawnItemType.name);

        HellSpawn.SpawnMovingParameters spawnMovingParameters = new HellSpawn.SpawnMovingParameters();
        spawnMovingParameters.flyTime = GetFlyTime(spawnPositionGameObject);
        spawnMovingParameters.hellSpawnSpeed = GetHellSpawnTimeScale();

        SpawnObjectAndStartMoving(spawnItemGameObject, spawnPositionGameObject, spawnMovingParameters);
        return spawnPositionGameObject;
    }

    private GameObject SpawnChain(SpawnItemType spawnItemType, List<GameObject> spawnPositions = null)
    {
        _spawnChainId++;
        if (_spawnChainId >= 20) {
            _spawnChainId = 0;
        }

        int spawnChainAmount = Random.Range(5, 12);        
        _spawnChainAmount[_spawnChainId] = spawnChainAmount;
        _spawnChainCount[_spawnChainId] = 0;
        
        CurrentSpawnChain _currentSpawnChain = new CurrentSpawnChain();
        
        _currentSpawnChain.amountLeft = spawnChainAmount;
        _currentSpawnChain.spawnItemType = spawnItemType;
        _currentSpawnChain.spawnItemGameObject = GetRandomSpawnPositionGameObject(spawnPositions);
        _currentSpawnChain.spawnItemUpGameObject = GetSpawnPositionUpGameObject(_currentSpawnChain.spawnItemGameObject);
        _currentSpawnChain.spawnItemDownGameObject = GetSpawnPositionDownGameObject(_currentSpawnChain.spawnItemGameObject);

        //_currentSpawnChain.spawnOrder = CreateRandomSpawnOrderForChain(spawnChainAmount);
        
        _currentSpawnChain.amountTotal = spawnChainAmount;        
        SequenceSpawnChain(_currentSpawnChain);
        return _currentSpawnChain.spawnItemGameObject;
    }

    private float prevTime = 0.0f;
    
    private void SpawnChainNext(CurrentSpawnChain _currentSpawnChain, Sequence spawnChainSequence)
    {            
        bool isLastInChain = (spawnChainSequence.Loops() - 1) == spawnChainSequence.CompletedLoops() ? true : false;        

        HellSpawn.SpawnMovingParameters spawnMovingParameters = new HellSpawn.SpawnMovingParameters();
        spawnMovingParameters.flyTime = GetFlyTime(_currentSpawnChain.spawnItemGameObject);        
        spawnMovingParameters.hellSpawnSpeed = GetHellSpawnTimeScale();
        _currentSpawnChain.spawnMovingParameters = spawnMovingParameters;

        GameObject spawnItemGameObject = GetSpawnItemGameObject(_currentSpawnChain.spawnItemType.name);

        //detect position for item in chain
        /*
        GameObject _chainSpawnItemGameObject = null;
        switch (_currentSpawnChain.spawnOrder[spawnChainSequence.CompletedLoops()]) {
            case    "main":
                _chainSpawnItemGameObject = _currentSpawnChain.spawnItemGameObject;
                break;
            case    "up":
                _chainSpawnItemGameObject = _currentSpawnChain.spawnItemUpGameObject;
                break;
            case    "down":
                _chainSpawnItemGameObject = _currentSpawnChain.spawnItemDownGameObject;
                break;                
        }
        if (_chainSpawnItemGameObject == null) {
            _chainSpawnItemGameObject = _currentSpawnChain.spawnItemGameObject;
        }
        */

        GameObject _chainSpawnItemGameObject = _currentSpawnChain.spawnItemGameObject;
        
        SpawnObjectAndStartMoving(spawnItemGameObject, _chainSpawnItemGameObject, _currentSpawnChain.spawnMovingParameters);
        spawnItemGameObject.SendMessage("SetIsLastInChain", isLastInChain, SendMessageOptions.DontRequireReceiver);

        _currentSpawnChain.amountLeft--;
    } 

    private void SpawnObjectAndStartMoving(GameObject spawnItemGameObject, GameObject spawnPositionGameObject, 
                                           HellSpawn.SpawnMovingParameters spawnMovingParameters)
    {
        SpawnObject(spawnItemGameObject, spawnPositionGameObject);                  
        spawnItemGameObject.SendMessage("StartMoving", spawnMovingParameters);
    }

    private void SpawnObject(GameObject spawnItemGameObject, GameObject spawnPositionGameObject)
    {
        spawnItemGameObject.transform.position = spawnPositionGameObject.transform.position;
        spawnItemGameObject.transform.parent = gameObject.transform;
        spawnItemGameObject.SendMessage("PrepareSpawn", SendMessageOptions.DontRequireReceiver);        
        spawnItemGameObject.SendMessage("SetSpawnChainId", _spawnChainId, SendMessageOptions.DontRequireReceiver);
        spawnItemGameObject.SendMessage("StartFire", GetHellSpawnTimeScale(), SendMessageOptions.DontRequireReceiver);
        AddSpawnedObject(spawnItemGameObject);            
    }    

    private void SequenceSpawnNext()
    {        
        _spawnNextSequence = DOTween.Sequence();        
        _spawnNextSequence.AppendCallback(SpawnNext);
        _spawnNextSequence.AppendInterval(_spawnTime);
        _spawnNextSequence.SetLoops(-1);        
    }

    private void SequenceSpawnChain(CurrentSpawnChain _currentSpawnChain)
    {        
        int amount = _currentSpawnChain.amountTotal;
        Sequence _spawnChainNextSequence = DOTween.Sequence();
        for (int i = 0; i < amount; i++) {
            int j = i;
            
            bool isLastInChain = (amount - 1) == j ? true : false;     
            
            GameObject spawnItemGameObject = GetSpawnItemGameObject(_currentSpawnChain.spawnItemType.name);
            gameObject.transform.DOKill();
            GameObject _chainSpawnItemGameObject = _currentSpawnChain.spawnItemGameObject;
            
            SpawnObject(spawnItemGameObject, _chainSpawnItemGameObject);
            spawnItemGameObject.GetComponent<SpriteRenderer>().enabled = true;
            spawnItemGameObject.GetComponent<BoxCollider2D>().enabled = true;            
            
            spawnItemGameObject.SendMessage("SetIsLastInChain", isLastInChain, SendMessageOptions.DontRequireReceiver);

            HellSpawn.SpawnMovingParameters spawnMovingParameters = new HellSpawn.SpawnMovingParameters();
            spawnMovingParameters.flyTime = GetFlyTime(spawnItemGameObject);        
            spawnMovingParameters.hellSpawnSpeed = GetHellSpawnTimeScale();            
            
            Tween _chainItemTween = spawnItemGameObject.GetComponent<HellSpawn>().
                                                        GetMovingTween(spawnMovingParameters, false, true);
            
            _spawnChainNextSequence.Insert(_spawnChainTime * j, _chainItemTween);
            
            //_spawnChainNextSequence.InsertCallback(_spawnChainTime * j, () => SpawnChainNext(_currentSpawnChain, _spawnChainNextSequence));
            //_spawnChainNextSequence.AppendInterval(_spawnChainTime);            
        }
                     
        //_spawnChainNextSequence.SetLoops(_currentSpawnChain.amountTotal);
        _spawnChainNextSequence.SetId(SPAWN_CHAIN_SEQUENCE);
        _spawnChainNextSequence.timeScale = GetHellSpawnTimeScale();
        //_spawnChainNextSequence.SetRecyclable(true);        
    }

    private float GetFlyTime(GameObject spawnItemGameObject)
    {
        float posX = spawnItemGameObject.transform.position.x;
        float distanceX = posX * 2;
        float result = distanceX / _spawnFlySpeed;        
        return result;
    }

    private SpawnItemType GetRandomSpawnItemType(string itemGroup)
    {                
        List<SpawnItemType> _spawnItemTypes = _spawnItemGroup[itemGroup];
        int randomSpawnItemTypeIndex = Random.Range(0, _spawnItemTypes.Count);
        return _spawnItemTypes[randomSpawnItemTypeIndex];
    }

    private SpawnItemType GetNextSpawnItemType(string itemGroup)
    {
        if (!_spawnItemGroupNext.ContainsKey(itemGroup)) {
            _spawnItemGroupNext.Add(itemGroup, 0);
        } else {
            _spawnItemGroupNext[itemGroup]++;
        }
        if (_spawnItemGroupNext[itemGroup] >= _spawnItemGroup[itemGroup].Count) {
            _spawnItemGroupNext[itemGroup] = 0;
        }
        if (_spawnItemGroupNext[itemGroup] == 0) {
            var _rnd = new System.Random();
            _spawnItemGroup[itemGroup] = _spawnItemGroup[itemGroup].OrderBy(x => _rnd.Next()).ToList();
        }
        return _spawnItemGroup[itemGroup][_spawnItemGroupNext[itemGroup]];
    }

    private int GetRandomSpawnPositionId(List<GameObject> spawnPositions = null)
    {
        if (spawnPositions == null) {
            spawnPositions = spawnPositionGameObjects.ToList();
        }        
        int randomIndex = Random.Range(0, GetSpawnPositionGameObjectsAmount(spawnPositions));
        return randomIndex;
    }
    
    
    private GameObject GetRandomSpawnPositionGameObject(List<GameObject> spawnPositions = null)
    {        
        return spawnPositions[GetRandomSpawnPositionId(spawnPositions)];
    }

    private GameObject GetSpawnItemGameObject(string spawnItemType)
    {
        GameObject result = ObjectPool.Spawn(spawnItemType);
        result.SendMessage("ResetGameObject", SendMessageOptions.DontRequireReceiver);
        return result;
    }

    public int GetSpawnPositionGameObjectsAmount(List<GameObject> spawnPositions = null)
    {
        if (spawnPositions == null) {
            spawnPositions = spawnPositionGameObjects.ToList();
        }        
        return spawnPositions.Count;
    }

    public void PauseSpawner()
    {
        _isPaused = true;

        _spawnNextSequence.Pause();
        _speedupSpawnerSequence.Pause();
        DOTween.Pause(Spawner.SPAWN_CHAIN_SEQUENCE);
        DOTween.Pause(Spawner.SPAWN_GROUP_SEQUENCE);
        //_spawnChainNextSequence.Pause();
        _slowDownHellSpawnsSequence.Pause();

        DOTween.Pause(HellSpawn.HELLSPAWN_DOTWEEN_ID); //pause hellspawns
        DOTween.Pause(HellSpawnBullet.HELLSPAWN_BULLET_DOTWEEN_ID); //pause bullets
        GameObject[] hellSpawnEnemies = GameObject.FindGameObjectsWithTag(HellSpawn.HELLSPAWN_ENEMY_TAG);
        foreach (GameObject hellSpawnEnemy in hellSpawnEnemies) {
            hellSpawnEnemy.SendMessage("PauseHellSpawnGun", SendMessageOptions.DontRequireReceiver);
        }
    }

    public void UnpauseSpawner()
    {        
        _isPaused = false;        
        DOTween.Play(HellSpawn.HELLSPAWN_DOTWEEN_ID);
        DOTween.Play(HellSpawnBullet.HELLSPAWN_BULLET_DOTWEEN_ID); //unpause bullets
        DOTween.Play(Spawner.SPAWN_GROUP_SEQUENCE);

        _spawnNextSequence.Play();
        _speedupSpawnerSequence.Play();
        DOTween.Play(Spawner.SPAWN_CHAIN_SEQUENCE);
        //_spawnChainNextSequence.Play();
        _slowDownHellSpawnsSequence.Play();

        GameObject[] hellSpawnEnemies = GameObject.FindGameObjectsWithTag(HellSpawn.HELLSPAWN_ENEMY_TAG);
        foreach (GameObject hellSpawnEnemy in hellSpawnEnemies) {
            hellSpawnEnemy.SendMessage("UnpauseHellSpawnGun", SendMessageOptions.DontRequireReceiver);
        }

    }

    public void StopSpawner()
    {
        _spawnNextSequence.Kill();
        _speedupSpawnerSequence.Kill();
        DOTween.Kill(SPAWN_CHAIN_SEQUENCE); 
        DOTween.Kill(Spawner.SPAWN_GROUP_SEQUENCE);
        SlowdownHellspawns();

        DOTween.Kill(HellSpawn.HELLSPAWN_DOTWEEN_ID);
        ClearSpawnedObjects();
    }

    public void addChainCount(int spawnChainId)
    {
        _spawnChainCount[spawnChainId]++;
    }

    public bool isFullChainCheck(int spawnChainId)
    {
        if (_spawnChainCount[spawnChainId] == _spawnChainAmount[spawnChainId]) {
            _isPreviousChainFull = true;
            return true;
        }
        _isPreviousChainFull = false;
        return false;
    }

    public bool isPreviousFullChainCheck()
    {
        return _isPreviousChainFull;        
    }

    public int getChainAmount(int spawnChainId)
    {
        return _spawnChainAmount[spawnChainId];
    }

    public void SpeedupHellspawns()
    {
        StopSlowDownHellSpawnsSequence();
        IsSpawnerSpeedup = true;
        
        UpdateHellSpawnsTimeScale();
        UpdateChainSpawnTimeScale();
        UpdateHellSpawnGunsTimeScale();

        SlowDownHellSpawnsSequence();

        playerBonusStatus.StartBonusSpeedup(_slowDownTimeBonus);
        
    }

    public void SlowdownHellspawns()
    {
        IsSpawnerSpeedup = false;
        
        UpdateHellSpawnsTimeScale();
        UpdateChainSpawnTimeScale();
        UpdateHellSpawnGunsTimeScale();
        
        StopSlowDownHellSpawnsSequence();
    }

    private void SlowDownHellSpawnsSequence()
    {
        _slowDownHellSpawnsSequence = DOTween.Sequence();
        _slowDownHellSpawnsSequence.AppendInterval(_slowDownTimeBonus);
        _slowDownHellSpawnsSequence.AppendCallback(SlowdownHellspawns);        
    }
    
    public void StopSlowDownHellSpawnsSequence()
    {
        _slowDownHellSpawnsSequence.Kill();
        playerBonusStatus.StopBonusSpeedup();
    }

    public List<Tween> GetAllHellspawnTweens()
    {        
        List<Tween> hellspawns = DOTween.TweensById(HellSpawn.HELLSPAWN_DOTWEEN_ID, true);
        List<Tween> hellspawnBullets = DOTween.TweensById(HellSpawnBullet.HELLSPAWN_BULLET_DOTWEEN_ID, true);
        List<Tween> result = new List<Tween>();
        if (hellspawns != null) {
            result.AddRange(hellspawns);
        }
        if (hellspawnBullets != null) {
            result.AddRange(hellspawnBullets);
        }
        return result;
    }

    public static void AddSpawnedObject(GameObject SpanwedGameObject)
    {
        _spawnedObjects.Add(SpanwedGameObject, true);
    }

    public static void RemoveSpawnedObject(GameObject SpanwedGameObject)
    {
        _spawnedObjects.Remove(SpanwedGameObject);
    }

    public static void ClearSpawnedObjects()
    {
        foreach (GameObject spawnedGameObject in _spawnedObjects.Keys) {
            spawnedGameObject.SendMessage("RecycleOnly");
        }
        _spawnedObjects.Clear();
    }

    private bool RollGlobalBlock()
    {        
        float randomDrop = Random.Range(0.0f, _overallWeight);        
        return (randomDrop <= _globalBlockWeight);
    }

    private string RollRandomItemGroupName(Dictionary<string, int> spawnItemGroupWeights)
    {
        string result = "";
        int sumWeight = 0;
        foreach (var spawnItemGroupWeight in spawnItemGroupWeights) {
            sumWeight += spawnItemGroupWeight.Value;
        }

        int randomDrop = Random.Range(0, sumWeight);        
        int previousWeightSum = 0;        
        foreach (var spawnItemGroupWeight in spawnItemGroupWeights) {
            if (randomDrop < previousWeightSum+spawnItemGroupWeight.Value && randomDrop >= previousWeightSum) {
                result = spawnItemGroupWeight.Key;                
            }
            previousWeightSum += spawnItemGroupWeight.Value;
        }
        return result;
    }

    private int GetRandomItemsAmount()
    {
        int result = Random.Range(2, 4);
        return result;
    }

    private GameObject GetSpawnPositionUpGameObject(GameObject mainPosition)
    {
        int positionIndex = spawnPositionGameObjects.ToList().IndexOf(mainPosition);
        if (positionIndex > 0) {
            return spawnPositionGameObjects[positionIndex - 1];
        }
        return null;
    }

    private GameObject GetSpawnPositionDownGameObject(GameObject mainPosition)
    {
        int positionIndex = spawnPositionGameObjects.ToList().IndexOf(mainPosition);
        if (positionIndex < spawnPositionGameObjects.Length - 1) {
            return spawnPositionGameObjects[positionIndex + 1];
        }
        return null;
    }

    private void SetChainSpawnTimeScale(float newTimeScale)
    {
        List <Tween> chainTweens = DOTween.TweensById(SPAWN_CHAIN_SEQUENCE);
        if (chainTweens == null) {
            return;
        }
        foreach (var tween in chainTweens) {
            tween.timeScale = newTimeScale;
        }
    }   

    public float GetHellSpawnTimeScale()
    {
        float result = IsSpawnerSpeedup ? _spawnFlyTimeScale * HellSpawnSpeedUp : _spawnFlyTimeScale;        
        return result;
    }

    private void UpdateTimeScales()
    {        
        _spawnNextSequence.timeScale = _spawnTimeScale;
        UpdateChainSpawnTimeScale();
        UpdateHellSpawnsTimeScale();
        UpdateHellSpawnGunsTimeScale();
    }

    private void UpdateHellSpawnsTimeScale()
    {
        List<Tween> hellspawnTweens = GetAllHellspawnTweens();
        foreach (Tween hellspawnTween in hellspawnTweens) {
            hellspawnTween.timeScale = GetHellSpawnTimeScale();
        }        
    }

    private void UpdateHellSpawnGunsTimeScale()
    {
        float _updateTimeScale = GetHellSpawnTimeScale();
        foreach (GameObject spawnedGameObject in _spawnedObjects.Keys) {
            spawnedGameObject.SendMessage("UpdateFire", _updateTimeScale, SendMessageOptions.DontRequireReceiver);
        }            
    }    

    private void UpdateChainSpawnTimeScale()
    {        
        List <Tween> chainTweens = DOTween.TweensById(SPAWN_CHAIN_SEQUENCE);
        if (chainTweens == null) {
            return;
        }        
        foreach (var tween in chainTweens) {
            tween.timeScale = GetHellSpawnTimeScale();
        }        
    }
    
    /**
     * @TODO i really don't like how it looks like with all its nested ifs, decrease nests
     */
    private List<string> CreateRandomSpawnOrderForChain(int amount)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < amount; i++) {
            if (i == 0) { //first one is always main                
                result.Add("main");
            } else {
                string previous = result[i - 1];
                string newItem = "";
                if (previous == "main") {
                    int rand = Random.Range(0, 3);
                    if (rand == 0) {
                        newItem = "main";
                    }
                    if (rand == 1) {
                        newItem = "up";
                    }
                    if (rand == 2) {
                        newItem = "down";
                    }
                }
                if (previous == "up") {
                    int rand = Random.Range(0, 2);
                    if (rand == 0) {
                        newItem = "up";
                    }
                    if (rand == 1) {
                        newItem = "main";
                    }                    
                }
                if (previous == "down") {
                    int rand = Random.Range(0, 2);
                    if (rand == 0) {
                        newItem = "down";
                    }
                    if (rand == 1) {
                        newItem = "main";
                    }                    
                }
                result.Add(newItem);
            }            
        }
        return result;
    }
}
