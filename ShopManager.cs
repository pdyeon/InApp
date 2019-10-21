using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Security;
using UnityEngine.Analytics;

public class ShopManager : MonoBehaviour, IStoreListener
{
    static IStoreController storeController = null;
    static string[] sProductIds;

    void Awake()
    {
        if (storeController == null)
        {
            sProductIds = new string[] { "adblock_1000" };
            InitStore();
        }
    }

    void InitStore()
    {
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(sProductIds[0], ProductType.Consumable, new IDs { { sProductIds[0], GooglePlay.Name },{ sProductIds[0], AppleAppStore.Name } });

        UnityPurchasing.Initialize(this, builder);
    }

    void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {

    }

    public void OnBtnPurchaseClicked(int index)
    {
        if (storeController == null)
        {

        }
        else
        {
            storeController.InitiatePurchase(sProductIds[index]);
        }
    }

    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs e)
    {
        bool isSuccess = true;
//#if UNITY_ANDROID && !UNITY_EDITOR
//		CrossPlatformValidator validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
//		try
//		{
//			IPurchaseReceipt[] result = validator.Validate(e.purchasedProduct.receipt);
//			for(int i = 0; i < result.Length; i++)
//				Analytics.Transaction(result[i].productID, e.purchasedProduct.metadata.localizedPrice, e.purchasedProduct.metadata.isoCurrencyCode, result[i].transactionID, null);
//		}
//		catch (IAPSecurityException)
//		{
//			isSuccess = false;
//		}
//#endif
        if (isSuccess)
        {
            //Debug.Log("구매 완료");
            if (e.purchasedProduct.definition.id.Equals(sProductIds[0]))
            {
                ProductManager.instance.ADBlockTrue();
            }
        }
        else
        {

        }

        return PurchaseProcessingResult.Complete;
    }

    void IStoreListener.OnPurchaseFailed(Product i, PurchaseFailureReason error)
    {
        if (!error.Equals(PurchaseFailureReason.UserCancelled))
        {

        }
    }
}
