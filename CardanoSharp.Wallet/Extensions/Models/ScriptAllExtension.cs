﻿using CardanoSharp.Wallet.Common;
using CardanoSharp.Wallet.Models.Transactions.Scripts;
using PeterO.Cbor2;
using System;
using System.Collections.Generic;
using System.Text;

namespace CardanoSharp.Wallet.Extensions.Models
{
    public static class ScriptAllExtension
    {
        public static byte[] GetPolicyId(this ScriptAll scriptAll)
        {
            var serializedCBOR = scriptAll.GetCBOR().EncodeToBytes();

            BigEndianBuffer buffer = new BigEndianBuffer();
            buffer.Write(new byte[] { 0x00 });
            buffer.Write(serializedCBOR);
            return HashHelper.Blake2b244(buffer.ToArray());
        }

        public static CBORObject GetCBOR(this ScriptAll scriptAll)
        {
            //script_all = (1, [*native_script])
            var scriptAllCbor = CBORObject.NewArray()
                .Add(1);

            foreach(var nativeScript in scriptAll.NativeScripts)
            {
                scriptAllCbor.Add(nativeScript.GetCBOR());
            }

            return scriptAllCbor;
        }
    }
}
