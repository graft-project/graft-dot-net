using System.ComponentModel;

namespace Graft.Infrastructure
{
    public enum ErrorCode
    {
        [Description("The amount cannot be less or equal to zero.")]
        InvalidAmount = 1,

        [Description("Sale currency '{0}' is not supported")]
        SaleCurrencyNotSupported = 2,

        [Description("Pay currency '{0}' is not supported")]
        PayCurrencyNotSupported = 3,

        [Description("Sale currency code must be set.")]
        SaleCurrencyEmpty = 4,

        [Description("Pay currency code must be set.")]
        PayCurrencyEmpty = 5,

        [Description("Payment not found or expired")]
        PaymentNotFoundOrExpired = 6,

        [Description("Service provider wallet address cannot be empty")]
        ServiceProviderWalletEmpty = 7,

        [Description("Service Provider Fee must be in range 0 - 0.2 (20%)")]
        InvalidServiceProviderFee = 8,

        [Description("Merchant wallet address cannot be empty")]
        MerchantWalletEmpty = 9,

        [Description("Payment '{0}' already started")]
        PaymentAlreadyStarted = 10,

        [Description("Cannot connect to the Exchange Broker")]
        NoConnectionToBroker = 11,

        [Description("Internal Server Error: {0}")]
        InternalServerError = 12,

        [Description("Terminal not found")]
        TerminalNotFound = 13,

        [Description("Store not found")]
        StoreNotFound = 14,

        [Description("Merchant not found")]
        MerchantNotFound = 15,

        [Description("Service provider not found")]
        ServiceProviderNotFound = 16,

        [Description("PaymentId must be set")]
        PaymentIdRequired = 17,

        [Description("Cannot conect to the DAPI node")]
        NoConnectionToDapi = 18,

        [Description("DAPI call has returned a non-known status ({0})")]
        UnknownDapiPaymentStatus = 19,

        [Description("DAPI call has returned an error. {0}")]
        DapiError = 20,

        [Description("If FiatCurrency is set you must set either SellFiatAmount or BuyFiatAmount")]
        FiatAmountEmpty = 21,

        [Description("Only one of the amount fields can be set")]
        OnlyOneAmountAllowed = 22,

        [Description("Sell currency cannot be empty")]
        SellCurrencyEmpty = 24,

        [Description("Sell currency '{0}' is not supported")]
        SellCurrencyNotSupported = 25,

        [Description("Buy currency cannot be empty")]
        BuyCurrencyEmpty = 26,

        [Description("Buy currency '{0}' is not supported")]
        BuyCurrencyNotSupported = 27,

        [Description("Wallet address cannot be empty")]
        WalletEmpty = 28,

        [Description("Exchange not found or expired")]
        ExchangeNotFoundOrExpired = 29,

        [Description("ExchangeId cannot be empty")]
        ExchangeIdEmpty = 30,

        [Description("PaymentId cannot be empty")]
        PaymentIdEmpty = 31,

        [Description("Terminal disabled")]
        TerminalDisabled = 32,

        [Description("Terminal disabled by service provider")]
        TerminalDisabledByServiceProvider = 33,

        [Description("Store disabled")]
        StoreDisabled = 34,

        [Description("Merchant disabled")]
        MerchantDisabled = 35,

        [Description("Service Provider disabled")]
        ServiceProviderDisabled = 36,

        [Description("Cannot conect to the Wallet RPC")]
        NoConnectionToRpc = 37,

        [Description("RPC call has returned an error. {0}")]
        RpcError = 38,

        [Description("Not enough money")]
        NotEnoughMoney = 39,

        [Description("Not enough unlocked balance")]
        NotEnoughUnlockedBalance = 40,

        [Description("Cannot get a pay wallet from the pool")]
        CannotGetPayWalletFromPool = 41,

        [Description("Cannot get a receive wallet from the pool")]
        CannotGetRecvWalletFromPool = 42,

        [Description("Invalid API Key")]
        InvalidApiKey = 43,

        [Description("Virtual Terminal not found")]
        VirtualTerminalNotFound = 44,

        [Description("Payment already made")]
        PaymentAlreadyMade = 45,

        [Description("Currency pair '{0}' is not supported")]
        CurrencyPairNotSupported = 46,
    }
}
