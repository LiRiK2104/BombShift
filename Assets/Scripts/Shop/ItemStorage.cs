using System.Collections.Generic;
using System.Linq;
using Shop.Items;

public class ItemStorage : Singleton<ItemStorage>
{
    private List<CurrencyContainer> _currencyContainers = new List<CurrencyContainer>();
    private List<Skin> _skins = new List<Skin>();

    
    public void Add(Skin skin)
    {
        if (HasSkin(skin) == false)
            _skins.Add(skin);
    }
    
    public void Add(Currency currency, int count)
    {
        CurrencyContainer container;
            
        if (HasCurrency(currency, out container))
        {
            container.Add(count);
        }
        else
        {
            container = new CurrencyContainer(currency, count);
            _currencyContainers.Add(container);
        }
    }
    
    public void Remove(Skin skin)
    {
        _skins.Remove(skin);
    }
    
    public void Remove(Currency currency, int count)
    {
        if (HasCurrency(currency, out CurrencyContainer container))
        {
            container.Remove(count);

            if (container.Count == 0)
                _currencyContainers.Remove(container);
        }
    }

    public bool HasSkin(Skin skin)
    {
        return _skins.Contains(skin);
    }

    public bool TryGetCurrencyCount(Currency currency, out int count)
    {
        count = 0;
        
        if (HasCurrency(currency, out CurrencyContainer currencyContainer))
        {
            count = currencyContainer.Count;
            return true;
        }

        return false;
    }

    private bool HasCurrency(Currency currency, out CurrencyContainer foundContainer)
    {
        foundContainer = _currencyContainers.FirstOrDefault(container => container.Currency == currency);
        return foundContainer == null;
    }
}
