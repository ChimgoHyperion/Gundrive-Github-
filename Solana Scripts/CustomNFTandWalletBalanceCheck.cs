using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Solnet.Wallet;
using Solnet.Rpc.Core.Http;
using Solnet;
using AllArt.Solana.Utility;
using dotnetstandard_bip39;
using Merkator.BitCoin;
using Solnet.Programs;
using Solnet.Rpc;
using Solnet.Rpc.Builders;
using Solnet.Rpc.Messages;
using Solnet.Rpc.Models;
using System;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UI;
using System.Linq;

public class CustomNFTandWalletBalanceCheck : MonoBehaviour
{
    private SolanaRpcClient rpcClient;
    private string userwalletaddress;
    private string nftcollectioncontractaddress;

    // Start is called before the first frame update
    void Start()
    {
        rpcClient = new SolanaRpcClient("https://api.mainnet-beta.solana.com");
    }

    void CheckNFTs()
    {
       
       
    }
   
    // Update is called once per frame
    void Update()
    {
        
    }
}
