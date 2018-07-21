using UnityEngine;
using System.Collections;

public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{

#if DEBUG_LOADING
	public void DebugLogMemory(string strAdditional)
	{
#if UNITY_EDITOR
		Debug.Log("MEMMEMMEM:: " + strAdditional + " MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB)");
#elif UNITY_PS3
		Debug.Log("MEMMEMMEM:: " + strAdditional + " MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB) FreeSystemMem: ("+ ((float)PS3SystemUtility.GetFreeSystemMem()/(1048576f)).ToString("0.000")+"MB)");
#endif
    }
#endif

#if DEBUG_LOADING
	public void DebugLogMemory()
	{
#if UNITY_EDITOR
		Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB)");
#elif UNITY_PS3
		Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB) FreeSystemMem: ("+ ((float)PS3SystemUtility.GetFreeSystemMem()/(1048576f)).ToString("0.000")+"MB)");
#endif
    }
#endif

    public static bool IsDestroyed()
    {
        return (m_Instance == null);
    }

    private static T m_Instance = null;
    public static T Instance
    {
        get
        {
            // Instance requiered for the first time, we look for it
            if (m_Instance == null)
            {
#if DEBUG_SINGLETONS
                Debug.Log(typeof(T).ToString() + ": m_Instance == null!");
#endif

#if DEBUG_LOADING
				Debug.Log ("Instance "+typeof(T).ToString() +" ++++++Started");
				
#if UNITY_EDITOR
				Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB)");
#elif UNITY_PS3
				Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB) FreeSystemMem: ("+ ((float)PS3SystemUtility.GetFreeSystemMem()/(1048576f)).ToString("0.000")+"MB)");
#endif

#endif
                m_Instance = GameObject.FindObjectOfType(typeof(T)) as T;

                // Object not found, we create a temporary one
                if (m_Instance == null)
                {
#if DEBUG_SINGLETONS
                    Debug.LogWarning("No instance of " + typeof(T).ToString() + ", a temporary one is created.");
#endif
                    m_Instance = new GameObject("Generated MonoSingleton: " + typeof(T).ToString(), typeof(T)).GetComponent<T>();


#if DEBUG_LOADING
					Debug.Log ("Instance Created"+typeof(T).ToString() +" ++++++Created");
					
#if UNITY_EDITOR
					Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB)");
#elif UNITY_PS3
					Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB) FreeSystemMem: ("+ ((float)PS3SystemUtility.GetFreeSystemMem()/(1048576f)).ToString("0.000")+"MB)");
#endif

#endif

#if DEBUG_SINGLETONS
                    // Problem during the creation, this should not happen
                    if (m_Instance == null)
                    {
                        Debug.LogError("Problem during the creation of " + typeof(T).ToString());
                    }
#endif
                }
                else//DF- Awake called if new object is created.
                {
                    DontDestroyOnLoad(m_Instance);
                    m_Instance.Init();
                }
            }
            return m_Instance;
        }
    }
    // If no other monobehaviour request the instance in an awake function
    // executing before this one, no need to search the object.
    private void Awake()
    {
        if (m_Instance == null)
        {
#if DEBUG_SINGLETONS
            Debug.Log(typeof(T).ToString() + ": A New Instance Awakens!");
#endif
            m_Instance = this as T;

            DontDestroyOnLoad(m_Instance);
            m_Instance.Init();
        }
        else
        {
            Destroy(gameObject);
        }

#if DEBUG_LOADING
		Debug.Log ("Instance Awoken"+typeof(T).ToString() +" ++++++Awake");

#if UNITY_EDITOR
		Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB)");
#elif UNITY_PS3
		Debug.Log("MEMMEMMEM:: MonoHeap: (" + ((float)Profiler.GetMonoHeapSize()/(1048576f)).ToString("0.000") + "MB) MonoUsed: (" + ((float)Profiler.GetMonoUsedSize()/(1048576f)).ToString("0.000") + "MB) TotAlloc: (" + ((float)Profiler.GetTotalAllocatedMemory()/(1048576f)).ToString("0.000") + "MB) TotReserved: (" + ((float)Profiler.GetTotalReservedMemory()/(1048576f)).ToString("0.000") + "MB) TotUnusedRes: (" + ((float)Profiler.GetTotalUnusedReservedMemory()/(1048576f)).ToString("0.000")+"MB) FreeSystemMem: ("+ ((float)PS3SystemUtility.GetFreeSystemMem()/(1048576f)).ToString("0.000")+"MB)");
#endif

#endif
    }

    //HACK - Dave A
#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (!Application.isPlaying)
        {
            if (gameObject.name.ToLower().Contains("temp"))
                DestroyImmediate(gameObject);
        }
    }
#endif

    // This function is called when the instance is used the first time
    // Put all the initializations you need here, as you would do in Awake
    public virtual void Init()
    {
#if DEBUG_SINGLETONS
        Debug.Log(typeof(T).ToString() + " Init not overridden");
#endif
    }

    // Make sure the instance isn't referenced anymore when the user quit, just in case.
    public void OnApplicationQuit()
    {
        ApplicationQuit();

#if UNITY_EDITOR
        //DestroyImmediate(gameObject);
#if DEBUG_SINGLETONS
       // Debug.Log(typeof(T).ToString() + " OnApplicationQuit not overridden");
#endif
#else
  //      Destroy(m_Instance.gameObject);
#endif
        m_Instance = null;
    }

    public virtual void ApplicationQuit()
    {
    }
}

