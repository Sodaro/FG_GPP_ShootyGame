using UnityEngine;

public class PlayerWeaponHandler : PlayerBaseComponent
{
    struct ShootArgs
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
    [SerializeField] GameObject _muzzleFlashObject = null;
    [SerializeField] Transform _muzzleTransform = null;
    #endregion

    #region Private Fields
    private AudioSource _weaponAudioSource;
    private Transform _cameraTransform;
    private PlayerInput _input;

    private const float MUZZLE_EFFECT_DURATION = 0.1f;

    private ShootArgs _unhandledShot;

    private float _muzzleTimer = 0;
    private float _range = 100f;

    private int _entityLayerMask;

    #endregion

    #region Public Methods
    public override void Initialize(PlayerBaseComponent component)
    {
        _weaponAudioSource = GetComponent<AudioSource>();
        _entityLayerMask = 1 << gameObject.layer;
        _cameraTransform = Camera.main.transform;
        _input = (PlayerInput)component;
    }
    public override void OnUpdate(float delta)
    {
        ////check timer to toggle flash effect off 
        //if (_muzzleTimer > 0)
        //{
        //    _muzzleTimer -= delta;
        //}
        //else
        //{
        //    if (_muzzleFlashObject.activeInHierarchy)
        //    {
        //        _muzzleFlashObject.SetActive(false);
        //    }
        //}

        //if (_input.Inputs.shootWasPressed)
        //{
        //    Vector3 startPos, endPos;
        //    startPos = _muzzleTransform.position;
        //    endPos = _cameraTransform.position + _cameraTransform.forward * _range;

        //    dirtyFlag = true;
        //    _unhandledShot = new ShootArgs(_cameraTransform.position, _cameraTransform.forward, _range, _entityLayerMask);

        //    _muzzleTimer = MUZZLE_EFFECT_DURATION;
        //    _muzzleFlashObject.SetActive(true);
        //    _weaponAudioSource.PlaySoundRandomPitch(0.75f, 1.1f);
        //    LinePool.Instance.Get().SetLinePositions(startPos, endPos);
        //}
    }

    public override void OnFixedUpdate(float fixedDelta)
    {
        //check timer to toggle flash effect off 
        if (_muzzleTimer > 0)
        {
            _muzzleTimer -= fixedDelta;
        }
        else
        {
            if (_muzzleFlashObject.activeInHierarchy)
            {
                _muzzleFlashObject.SetActive(false);
            }
        }

        if (_input.Inputs.shootWasPressed)
        {
            Vector3 startPos, endPos;
            startPos = _muzzleTransform.position;
            endPos = _cameraTransform.position + _cameraTransform.forward * _range;

            dirtyFlag = true;
            _unhandledShot = new ShootArgs(_cameraTransform.position, _cameraTransform.forward, _range, _entityLayerMask);

            _muzzleTimer = MUZZLE_EFFECT_DURATION;
            _muzzleFlashObject.SetActive(true);
            _weaponAudioSource.PlaySoundRandomPitch(0.75f, 1.1f);
            LinePool.Instance.Get().SetLinePositions(startPos, endPos);

            HandleShot(_unhandledShot);
            //dirtyFlag = false;

        }
    }
    #endregion

    #region Private Methods
    private void HandleShot(ShootArgs args)
    {
        if (Physics.Raycast(args.pos, args.dir, out RaycastHit hit, args.range, _entityLayerMask, QueryTriggerInteraction.Ignore))
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            Transform bodyParent = hit.collider.attachedRigidbody.transform.parent;
            if (body == null || bodyParent == null)
                return;

            if (hit.collider.attachedRigidbody.transform.parent.TryGetComponent(out Enemy enemy))
            {
                enemy.Kill((transform.position - hit.point).normalized);
            }
        }
    }
    #endregion
}
