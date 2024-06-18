using Fishing.Stats;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace Logbook
{
    public class LogBookPageManager : MonoBehaviour
    {
        public static LogBookPageManager instance;

        [SerializeField] private LogbookPage prefab;
        [SerializeField] private LogbookPage emptyPage;
        [SerializeField] private Transform left;
        [SerializeField] private Transform right;
        public List<LogbookPage> pages;

        int currentPageIndex;
        private void Awake()
        {
            instance = this;
        }
        public void MakePages()
        {
            Debug.Log("making opages");
            foreach (var item in LogBook.instance.fishDictionary)
            {
                Transform pageAlign = pages.Count % 2 == 0 ? left : right;
                LogbookPage newPage = Instantiate(prefab, pageAlign.localPosition, Quaternion.identity, pageAlign);
                newPage.transform.localPosition = new Vector3(50, 50, 0);
                SetupPage(newPage, item.Key);
            }

            int indexLoop = pages.Count;
            if (pages.Count % 2 == 1)
            {
                LogbookPage newPage = Instantiate(emptyPage, right.position, Quaternion.identity, right);
                newPage.transform.localPosition = new Vector3(50, 50, 0);
                pages.Add(newPage);
            }
            for (int i = 0; i < pages.Count; i++)
            {
                if (!(i == 0 || i == 1)) pages[i].gameObject.SetActive(false);
            }
            currentPageIndex = 1;

            for (int i = 0; i < indexLoop; i++)
            {
                if (i % 2 == 0 && i <= pages.Count) pages[i].nextPageButton.gameObject.SetActive(false);
                if (i % 2 == 1 && i <= pages.Count || i == 0) pages[i].previousPageButton.gameObject.SetActive(false);
            }
        }
        private void SetupPage(LogbookPage page, FishType type)
        {
            page.img.sprite = type.fishUnknownSprite;
            page.title.text = type.fishName;
            page.description.text = type.description;
            page.count.text = 0.ToString();
            page.previousPageButton.onClick.AddListener(PreviousPage);
            page.nextPageButton.onClick.AddListener(NextPage);
            pages.Add(page);
        }
        public void NextPage()
        {
            pages[currentPageIndex + 1].gameObject.SetActive(true);
            pages[currentPageIndex + 2].gameObject.SetActive(true);

            pages[currentPageIndex].gameObject.SetActive(false);
            pages[currentPageIndex - 1].gameObject.SetActive(false);
            currentPageIndex += 2;
        }
        public void PreviousPage()
        {

            pages[currentPageIndex - 2].gameObject.SetActive(true);
            pages[currentPageIndex - 3].gameObject.SetActive(true);

            pages[currentPageIndex].gameObject.SetActive(false);
            pages[currentPageIndex - 1].gameObject.SetActive(false);
            currentPageIndex -= 2;
        }
    }
}
