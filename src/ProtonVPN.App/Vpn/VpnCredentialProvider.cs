﻿/*
 * Copyright (c) 2020 Proton Technologies AG
 *
 * This file is part of ProtonVPN.
 *
 * ProtonVPN is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * ProtonVPN is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with ProtonVPN.  If not, see <https://www.gnu.org/licenses/>.
 */

using ProtonVPN.Common.Vpn;
using ProtonVPN.Core.Settings;

namespace ProtonVPN.Vpn
{
    public class VpnCredentialProvider
    {
        private readonly IAppSettings _appSettings;
        private readonly IUserStorage _userStorage;

        public VpnCredentialProvider(IAppSettings appSettings, IUserStorage userStorage)
        {
            _userStorage = userStorage;
            _appSettings = appSettings;
        }

        public VpnCredentials Credentials()
        {
            var user = _userStorage.User();

            return new VpnCredentials(GetUsername(_userStorage.User().VpnUsername), user.VpnPassword);
        }

        private string GetUsername(string username)
        {
            // p - proton, w - windows
            username += "+pw";

            if (_appSettings.FeatureNetShieldEnabled && _appSettings.NetShieldEnabled)
            {
                username += $"+f{_appSettings.NetShieldMode}";
            }

            return username;
        }
    }
}
