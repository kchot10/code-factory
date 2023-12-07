using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
public class StageItem : MonoBehaviour
{
    
    private Animation _animation;
    private Animator _animator;
    private Rigidbody _rigidbody;
    
    [SerializeField] private StageItemManager.StageItemType StageItemtype;
    [SerializeField] private ParticleSystem itemParticleSystem;
    [SerializeField] private AudioSource itemAudioSource;
    
    private event UnityAction<StageItemManager.StageItemType> OnSelectItemEvent;


    private void Awake()
    {
        if (TryGetComponent(out Animation animation))
        {
            _animation = animation;
        }
        else
        {
            _animator = GetComponent<Animator>();
        }

        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        OnSelectItemEvent += GameManager.Instance.StageItemManager.OnSelectItemEvent;
    }

   
    [ContextMenu("Execute OnSelectItem")]
    public void OnSelectItem()
    {
        ControllerItemAnimation(false);
        PlayItemSound();
        PlayItemParticleSystem();
        _rigidbody.isKinematic = false;
        
        OnSelectItemEvent?.Invoke(StageItemtype);
        OnSelectItemEvent = null;
    }
    
    // 아이템 잡을 시
    public void OnSelectItem(SelectEnterEventArgs targetArgs)
    {
        ControllerItemAnimation(false);
        PlayItemSound();
        PlayItemParticleSystem();
        _rigidbody.isKinematic = false;
        
       OnSelectItemEvent?.Invoke(StageItemtype);
       OnSelectItemEvent = null;
    }

    // 아이템 애니메이션 컨트롤
    public void ControllerItemAnimation(bool isEnable)
    {
        if (_animation == null)
        {
            _animator.enabled = isEnable;
        }
        else
        {
            _animation.enabled = isEnable;
            _animation.Play();
        }
    }

    // 아이템 효과음 실행
    public void PlayItemSound()
    {
        itemAudioSource.loop = false;
        itemAudioSource.Play();
    }

    // 아이템 FX 효과 실행
    public void PlayItemParticleSystem()
    {
        itemParticleSystem.Play();
    }
}
