using UnityEngine;

public class PlayerWeaponHandler : PlayerBaseComponent
{
    private struct ShootArgs
    {
        public ShootArgs(Vector3 pos, Vector3 dir, float range, int mask)
        {
            this.pos = pos;
            this.dir = dir;
            this.range = range;
            this.mask = mask;
        }
        public Vector3 pos;
        public Vector3 dir;
        public float range;
        public int mask;
    };

    #region Serialized Fields
    [SerializeField] private GameObject _muzzleFlashObject = null;
    [SerializeField] private Transform _muzzleTransform = null;
    #endregion

    #region Private Fields
    private AudioSource _weaponAudioSource;
    private Transform _cameraTransform;

    private const float MUZZLE_EFFECT_DURATION = 0.1f;

    private ShootArgs _unhandledShot;

    private float _timeBetweenShots = 0.5f;
    private float _shootTimer = 0f;

    private float _muzzleTimer = 0;
    private float _range = 100f;

    private int _entityLayerMask;

    #endregion

    #region Public Methods
    public override void Initialize()
    {
        _weaponAudioSource = GetComponent<AudioSource>();
        _entityLayerMask = 1 << gameObject.layer;
        _cameraTransform = Camera.main.transform;
    }
    public override void OnUpdate(float delta)
    {
        if (_shootTimer > 0f)
        {
            _shootTimer -= delta;
        }

        //check timer to toggle flash effect off 
        if (_muzzleTimer > 0)
        {
            _muzzleTimer -= delta;
        }
        else
        {
            if (_muzzleFlashObject.activeInHierarchy)
            {
                _muzzleFlashObject.SetActive(false);
            }
        }
    }

    public override void OnFixedUpdate(float fixedDelta, in InputHandler.InputVars inputs)
    {
        if (inputs.shootWasPressed && _shootTimer <= 0)
        {
            Vector3 startPos, endPos;
            startPos = _muzzleTransform.position;
            endPos = _cameraTransform.position + _cameraTransform.forward * _range;

            _unhandledShot = new ShootArgs(_cameraTransform.position, _cameraTransform.forward, _range, _entityLayerMask);

            _muzzleTimer = MUZZLE_EFFECT_DURATION;
            _muzzleFlashObject.SetActive(true);
            _weaponAudioSource.PlaySoundRandomPitch(0.75f, 1.1f);

            BulletLine line = LinePool.Instance.Get();
            if (line != null)
            {
                line.SetLinePositions(startPos, endPos);
            }
            
            HandleShot(_unhandledShot);
            _shootTimer = _timeBetweenShots;
        }
    }
    #endregion

    #region Private Methods
    private void HandleShot(in ShootArgs args)
    {
        if (Physics.Raycast(args.pos, args.dir, out RaycastHit hit, args.range, _entityLayerMask, QueryTriggerInteraction.Ignore))
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            Transform bodyParent = hit.collider.attachedRigidbody.transform.parent;
            if (body == null || bodyParent == null)
                return;

            if (hit.collider.attachedRigidbody.transform.parent.TryGetComponent(out IHealth entity))
            {
                entity.ReduceHealth(20f);
            }
        }
    }
    #endregion
}
