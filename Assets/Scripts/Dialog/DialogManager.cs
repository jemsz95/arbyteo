using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManager : MonoBehaviour {
	
	public GameObject dialogBoxPrefab;
	public Canvas userInterface;
	public TextAsset dialogFile;

	private Dictionary<int, Line[]> dialogsMap;
	private DialogBox dialogBox;
	private int lineNumber;
	private Line[] activeDialog;
	private UnityAction lineCompletedCallback;
	private UnityAction dialogCompleteCallback;

	public void funStartDialog(int dialogId, UnityAction callback) {
		if(dialogBox == null) {
			dialogBox = Instantiate(dialogBoxPrefab, userInterface.transform)
				.GetComponent<DialogBox>();
		}
		
		lineNumber = 0;
		activeDialog = dialogsMap[dialogId];

		dialogBox.funLoadLineWithCallback(activeDialog[lineNumber], lineCompletedCallback);
		dialogCompleteCallback = callback;
	}

	private void Awake() {
		lineCompletedCallback = new UnityAction(LineCompleted);

		dialogsMap = new Dictionary<int, Line[]>();
		
		var json = dialogFile.text;
		var dialogsList = JsonUtility.FromJson<DialogList>(json);

		foreach(Dialog d in dialogsList.dialogs) {
			dialogsMap.Add(d.id, d.lines);
		}
	}

	private void LineCompleted() {
		lineNumber++;

		if(lineNumber < activeDialog.Length) {
			dialogBox.funLoadLineWithCallback(activeDialog[lineNumber], lineCompletedCallback);
		} else {
			Destroy(dialogBox.gameObject);

			if(dialogCompleteCallback != null) {
				dialogCompleteCallback.Invoke();
			}
		}
	}
}

//Disable null dialogs warning
#pragma warning disable 0649
[System.Serializable]
class DialogList {
	public Dialog[] dialogs;
}
#pragma warning restore 0649
