-- Octobre 2017
-- Export des articles d'un logiel d'une mediathèque

DECLARE @i as INT
DECLARE @iWHILE INT
DECLARE @iMAX INT
DECLARE @sSQL_SELECT_1 VARCHAR(MAX)
DECLARE @sSQL_FROM_1 VARCHAR(MAX)
DECLARE @sSQL_SELECT_2 VARCHAR(MAX)
DECLARE @sSQL_FROM_2 VARCHAR(MAX)
DECLARE @sSQL_SELECT_3 VARCHAR(MAX)
DECLARE @sSQL_FROM_3 VARCHAR(MAX)
DECLARE @sSQL_SELECT_4 VARCHAR(MAX)
DECLARE @sSQL_FROM_4 VARCHAR(MAX)
DECLARE @sSQL_SELECT_5 VARCHAR(MAX)
DECLARE @sSQL_FROM_5 VARCHAR(MAX)
DECLARE @iLignesMAX INT

SET @iLignesMAX = 100000
SET @i=0
SET @iMAX=0
SET @iWHILE = 0
SET @sSQL_SELECT_1 = N''
SET @sSQL_FROM_1 = N''
SET @sSQL_SELECT_2 = N''
SET @sSQL_FROM_2 = N''
SET @sSQL_SELECT_3 = N''
SET @sSQL_FROM_3 = N''
SET @sSQL_SELECT_4 = N''
SET @sSQL_FROM_4 = N''
SET @sSQL_SELECT_5 = N''
SET @sSQL_FROM_5 = N''
SET @iMAX= @iMAX + (
			SELECT COUNT([Audio].[iID])
			FROM [Audio]
			LEFT JOIN [Audio_Exemplaires] ON [Audio].[iID]=[Audio_Exemplaires].[iAudioID]
			LEFT JOIN [Sites] ON [Audio_Exemplaires].[iSiteID]=[Sites].[iID]
			LEFT JOIN [Audio_Supports] ON [Audio_Exemplaires].[iAudioSupportID]=[Audio_Supports].[iID]
			LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
			ON [Audio_Supports].[sSupportID]=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=[Audio_Exemplaires].[iSiteID]
			LEFT JOIN [Personnalites] ON [Audio].[iPersoID]=[Personnalites].[iID]
			LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
			LEFT JOIN [Audio_Maisons] ON [Audio].[iID]=[Audio_Maisons].[iAudioID]
			LEFT JOIN [Maisons] ON [Audio_Maisons].[iMaisonID]=[Maisons].[iID]
			LEFT JOIN [Genres] ON [Audio].[iGenreID]=[Genres].[iID]
			LEFT JOIN [Publics] ON [Audio].[iPublicID]=[Publics].[iID]
			INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]		
		)
SET @iMAX= @iMAX + (
			SELECT COUNT([Info].[iID])
			FROM [Info]
			LEFT JOIN [Info_Exemplaires] ON [Info].[iID]=[Info_Exemplaires].[iInfoID]
			LEFT JOIN [Sites] ON [Info_Exemplaires].[iSiteID]=[Sites].[iID]
			LEFT JOIN [Info_Supports] ON [Info_Exemplaires].[iInfoSupportID]=[Info_Supports].[iID]
			LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
			ON [Info_Supports].[sSupportID]=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=[Info_Exemplaires].[iSiteID]
			LEFT JOIN [Personnalites] ON [Info].[iPersoID]=[Personnalites].[iID]
			LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
			LEFT JOIN [Info_Maisons] ON [Info].[iID]=[Info_Maisons].[iInfoID]
			LEFT JOIN [Maisons] ON [Info_Maisons].[iMaisonID]=[Maisons].[iID]
			LEFT JOIN [Genres] ON [Info].[iGenreID]=[Genres].[iID]
			LEFT JOIN [Publics] ON [Info].[iPublicID]=[Publics].[iID]
			INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]		
		)
SET @iMAX= @iMAX + (
			SELECT COUNT([Livres].[iID])
			FROM [Livres]
			LEFT JOIN [Livres_Exemplaires] ON [Livres].[iID]=[Livres_Exemplaires].[iLivresID]
			LEFT JOIN [Sites] ON [Livres_Exemplaires].[iSiteID]=[Sites].[iID]
			LEFT JOIN [Livres_Supports] ON [Livres_Exemplaires].[iLivresSupportID]=[Livres_Supports].[iID]
			LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
			ON [Livres_Supports].[sSupportID]=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=[Livres_Exemplaires].[iSiteID]
			LEFT JOIN [Personnalites] ON [Livres].[iPersoID]=[Personnalites].[iID]
			LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
			LEFT JOIN [Livres_Maisons] ON [Livres].[iID]=[Livres_Maisons].[iLivresID]
			LEFT JOIN [Maisons] ON [Livres_Maisons].[iMaisonID]=[Maisons].[iID]
			LEFT JOIN [Genres] ON [Livres].[iGenreID]=[Genres].[iID]
			LEFT JOIN [Publics] ON [Livres].[iPublicID]=[Publics].[iID]
			INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
		)
SET @iMAX= @iMAX + (
			SELECT COUNT([Perio].[iID])
			FROM [Perio]
			LEFT JOIN [Perio_Exemplaires] ON [Perio].[iID]=[Perio_Exemplaires].[iPerioID]
			LEFT JOIN [Perio_Numeros] ON [Perio_Exemplaires].[iNumeroID]=[Perio_Numeros].[iID]
			LEFT JOIN [Douchette] ON [Perio_Exemplaires].[sCodeBarre]=[Douchette].[sCodeBarre]
			LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
			ON [Douchette].[iTypeID]=[Medias].[iID]
			LEFT JOIN [Sites] ON [Perio_Exemplaires].[iSiteID]=[Sites].[iID]
			LEFT JOIN [Genres] ON [Perio].[iGenreID]=[Genres].[iID]
			LEFT JOIN [Publics] ON [Perio].[iPublicID]=[Publics].[iID]
			LEFT JOIN [Personnalites] ON [Perio].[iPersoID]=[Personnalites].[iID]
			LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
			INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
		)
SET @iMAX= @iMAX + (
			SELECT COUNT([Video].[iID])
			FROM [Video]
			LEFT JOIN [Video_Exemplaires] ON [Video].[iID]=[Video_Exemplaires].[iVideoID]
			LEFT JOIN [Sites] ON [Video_Exemplaires].[iSiteID]=[Sites].[iID]
			LEFT JOIN [Video_Supports] ON [Video_Exemplaires].[iVideoSupportID]=[Video_Supports].[iID]
			LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
			ON ISNULL([Video_Supports].[sSupportID],[Medias_Supports].[sSupportID])=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=ISNULL([Video_Exemplaires].[iSiteID],[Medias_Supports].[iSiteID])
			LEFT JOIN [Personnalites] ON [Video].[iPersoID]=[Personnalites].[iID]
			LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
			LEFT JOIN [Video_Maisons] ON [Video].[iID]=[Video_Maisons].[iVideoID]
			LEFT JOIN [Maisons] ON [Video_Maisons].[iMaisonID]=[Maisons].[iID]
			LEFT JOIN [Genres] ON [Video].[iGenreID]=[Genres].[iID]
			LEFT JOIN [Publics] ON [Video].[iPublicID]=[Publics].[iID]
			INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
		)
--SELECT @iMAX
IF ((@iMAX % @iLignesMAX)>0)
BEGIN
	SET @iWHILE = (@iMAX/@iLignesMAX)+1 
END
ELSE
BEGIN
	SET @iWHILE = (@iMAX/@iLignesMAX)
END 
--SELECT @iWHILE

IF (@iWHILE>0)
BEGIN
	SET @i=0
	WHILE @i<@iWHILE
	BEGIN	
		PRINT N'-----------------------------------------------------------------------------------'
		PRINT (N'-- Lignes ' + CONVERT(VARCHAR,(@i*@iLignesMAX)+1) + ' à ' +  CASE WHEN CONVERT(VARCHAR,(((@i+1)*@iLignesMAX)))>@iMAX THEN CONVERT(VARCHAR,@iMAX) ELSE CONVERT(VARCHAR,(((@i+1)*@iLignesMAX))) END) COLLATE Latin1_General_CS_AS
		PRINT N'-----------------------------------------------------------------------------------'
		PRINT N'' + CHAR(13) + CHAR(10)
		SET @sSQL_SELECT_1 = N'SELECT *
					FROM (
					SELECT 
					ROW_NUMBER() OVER (ORDER BY [LIBELLE_MEDIA]) AS [RowNum] 
					, *
					FROM (
					SELECT
					[Audio].[iID]
					, [Medias].[sNom] AS [LIBELLE_FAMILLE]
					, REPLACE(REPLACE(REPLACE((LTRIM(RTRIM(([Audio].[sArticle] + LTRIM(RTRIM([Audio].[sTitre])))))), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [LIBELLE_MEDIA]
					, [Medias_Supports].[sNom] AS [DESCRIPTION_MEDIA]
					, ([Personnalites].[sNom] + (CASE WHEN ISNULL([Personnalites].[sPrenom],'''') = '''' THEN '''' ELSE '', '' + [Personnalites].[sPrenom] END)) AS [AUTEUR]
					, [Maisons].[sNom] AS [EDITEUR]
					, [Genres].[sNom] AS [GENRE]
					, [Nationalites].[sNom] AS [NATIONNALITE]
					, REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(MAX),[Audio].[mDescription]),'';'','',''), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [RESUME]
					, Null AS [CRITIQUE]
					, [Publics].[sNom] AS [CIBLE]
					, (CASE WHEN [Audio].[iAnnee]>0 THEN [Audio].[iAnnee] ELSE Null END) AS [DATE_DE_SORTIE]
					, Null AS [DUREE]
					, Null AS [NOTE]
					, [Audio_Exemplaires].[sCodeBarre] AS [LIBELLE_EXEMPLAIRE] 
					, [Audio_Exemplaires].[sCodeBarre] AS [REF_EXEMPLAIRE]
					, [Audio_Exemplaires].[sCodeBarre] AS [CODE_BARRE]
					, [Audio].[sCote] AS [COTE]
					, Null AS [THEME]
					, Null AS [COLLECTION]
					, Null AS [NUMEROTATION]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Audio_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=10 THEN [Audio_Supports].[sISBN] ELSE Null END) AS [ISBN10]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Audio_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=13 THEN [Audio_Supports].[sISBN] ELSE Null END) AS [ISBN13]
					, Null AS [DATE_EDITION]
					, [Medias].[iType] AS [TYPE_MEDIA]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Audio_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Audio_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) END) AS [EST_UNE_NOUVEAUTE]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Audio_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(d,[Sites].[iNouv],[Audio_Supports].[dDebCirculation])) ELSE Null END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Audio_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(M,[Sites].[iNouv],[Audio_Supports].[dDebCirculation])) ELSE Null END) END) AS [DATE_FIN_NOUVEAUTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Audio_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Audio].[iID] AND [Commandes].[sSupportID]=[Audio_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN 0 ELSE 1 END) AS [EN_FILE_D_ATTENTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Audio_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Audio].[iID] AND [Commandes].[sSupportID]=[Audio_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN DATEDIFF(d,GETDATE(),DATEADD(d,[Medias_Supports].[iDureeEmprunt],(SELECT TOP 1 dDebEmprunt FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Audio_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Audio].[iID] AND [Commandes].[sSupportID]=[Audio_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0))) ELSE [Medias_Supports].[iDureeEmprunt] END) AS [NOMBRE_DE_JOUR]
					, (CASE WHEN ([Audio_Supports].[vPrixValeur] * [Audio_Supports].[iQteTot])>0 THEN ([Audio_Supports].[vPrixValeur] * [Audio_Supports].[iQteTot]) ELSE Null END) AS [PRIX_ACHAT]
					, (CASE WHEN ([Audio_Supports].[vPrixValeur] * [Audio_Supports].[iQteTot])>0 THEN [Audio_Supports].[dCrea] ELSE Null END) AS [DATE_ACHAT]
					, Null AS [LIEU_ACHAT]'
		SET @sSQL_FROM_1 = N' FROM [Audio]
					LEFT JOIN [Audio_Exemplaires] ON [Audio].[iID]=[Audio_Exemplaires].[iAudioID]
					LEFT JOIN [Sites] ON [Audio_Exemplaires].[iSiteID]=[Sites].[iID]
					LEFT JOIN [Audio_Supports] ON [Audio_Exemplaires].[iAudioSupportID]=[Audio_Supports].[iID]
					LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
					ON [Audio_Supports].[sSupportID]=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=[Audio_Exemplaires].[iSiteID]
					LEFT JOIN [Personnalites] ON [Audio].[iPersoID]=[Personnalites].[iID]
					LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
					LEFT JOIN [Audio_Maisons] ON [Audio].[iID]=[Audio_Maisons].[iAudioID]
					LEFT JOIN [Maisons] ON [Audio_Maisons].[iMaisonID]=[Maisons].[iID]
					LEFT JOIN [Genres] ON [Audio].[iGenreID]=[Genres].[iID]
					LEFT JOIN [Publics] ON [Audio].[iPublicID]=[Publics].[iID]
					INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
					UNION'
		SET @sSQL_SELECT_2 = N' SELECT 
					[Info].[iID]
					, [Medias].[sNom] AS [LIBELLE_FAMILLE]
					, REPLACE(REPLACE(REPLACE((LTRIM(RTRIM(([Info].[sArticle] + LTRIM(RTRIM([Info].[sTitre])))))), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [LIBELLE_MEDIA]
					, [Medias_Supports].[sNom] AS [DESCRIPTION_MEDIA]
					, ([Personnalites].[sNom] + (CASE WHEN ISNULL([Personnalites].[sPrenom],'''') = '''' THEN '''' ELSE '', '' + [Personnalites].[sPrenom] END)) AS [AUTEUR]
					, [Maisons].[sNom] AS [EDITEUR]
					, [Genres].[sNom] AS [GENRE]
					, [Nationalites].[sNom] AS [NATIONNALITE]
					, REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(MAX),[Info].[mDescription]),'';'','',''), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [RESUME]
					, Null AS [CRITIQUE]
					, [Publics].[sNom] AS [CIBLE]
					, (CASE WHEN [Info].[iAnnee]>0 THEN [Info].[iAnnee] ELSE Null END) AS [DATE_DE_SORTIE]
					, Null AS [DUREE]
					, Null AS [NOTE]
					, [Info_Exemplaires].[sCodeBarre] AS [LIBELLE_EXEMPLAIRE] 
					, [Info_Exemplaires].[sCodeBarre] AS [REF_EXEMPLAIRE]
					, [Info_Exemplaires].[sCodeBarre] AS [CODE_BARRE]
					, [info].[sCote] AS [COTE]
					, Null AS [THEME]
					, Null AS [COLLECTION]
					, Null AS [NUMEROTATION]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Info_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=10 THEN [Info_Supports].[sISBN] ELSE Null END) AS [ISBN10]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Info_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=13 THEN [Info_Supports].[sISBN] ELSE Null END) AS [ISBN13]
					, Null AS [DATE_EDITION]
					, [Medias].[iType] AS [TYPE_MEDIA]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Info_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Info_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) END) AS [EST_UNE_NOUVEAUTE]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Info_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(d,[Sites].[iNouv],[Info_Supports].[dDebCirculation])) ELSE Null END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Info_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(M,[Sites].[iNouv],[Info_Supports].[dDebCirculation])) ELSE Null END) END) AS [DATE_FIN_NOUVEAUTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Info_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Info].[iID] AND [Commandes].[sSupportID]=[Info_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN 0 ELSE 1 END) AS [EN_FILE_D_ATTENTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Info_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Info].[iID] AND [Commandes].[sSupportID]=[Info_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN DATEDIFF(d,GETDATE(),DATEADD(d,[Medias_Supports].[iDureeEmprunt],(SELECT TOP 1 dDebEmprunt FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Info_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Info].[iID] AND [Commandes].[sSupportID]=[Info_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0))) ELSE [Medias_Supports].[iDureeEmprunt] END) AS [NOMBRE_DE_JOUR]
					, (CASE WHEN ([Info_Supports].[vPrixValeur] * [Info_Supports].[iQteTot])>0 THEN ([Info_Supports].[vPrixValeur] * [Info_Supports].[iQteTot]) ELSE Null END) AS [PRIX_ACHAT]
					, (CASE WHEN ([Info_Supports].[vPrixValeur] * [Info_Supports].[iQteTot])>0 THEN [Info_Supports].[dCrea] ELSE Null END) AS [DATE_ACHAT]
					, Null AS [LIEU_ACHAT]'
		SET @sSQL_FROM_2 = N' FROM [Info]
					LEFT JOIN [Info_Exemplaires] ON [Info].[iID]=[Info_Exemplaires].[iInfoID]
					LEFT JOIN [Sites] ON [Info_Exemplaires].[iSiteID]=[Sites].[iID]
					LEFT JOIN [Info_Supports] ON [Info_Exemplaires].[iInfoSupportID]=[Info_Supports].[iID]
					LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
					ON [Info_Supports].[sSupportID]=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=[Info_Exemplaires].[iSiteID]
					LEFT JOIN [Personnalites] ON [Info].[iPersoID]=[Personnalites].[iID]
					LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
					LEFT JOIN [Info_Maisons] ON [Info].[iID]=[Info_Maisons].[iInfoID]
					LEFT JOIN [Maisons] ON [Info_Maisons].[iMaisonID]=[Maisons].[iID]
					LEFT JOIN [Genres] ON [Info].[iGenreID]=[Genres].[iID]
					LEFT JOIN [Publics] ON [Info].[iPublicID]=[Publics].[iID]
					INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
					UNION'
		SET @sSQL_SELECT_3 = N' SELECT 
					[Livres].[iID]
					, [Medias].[sNom] AS [LIBELLE_FAMILLE]
					, REPLACE(REPLACE(REPLACE((LTRIM(RTRIM(([Livres].[sArticle] + LTRIM(RTRIM([Livres].[sTitre])))))), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [LIBELLE_MEDIA]
					, [Medias_Supports].[sNom] AS [DESCRIPTION_MEDIA]
					, ([Personnalites].[sNom] + (CASE WHEN ISNULL([Personnalites].[sPrenom],'''') = '''' THEN '''' ELSE '', '' + [Personnalites].[sPrenom] END)) AS [AUTEUR]
					, [Maisons].[sNom] AS [EDITEUR]
					, [Genres].[sNom] AS [GENRE]
					, [Nationalites].[sNom] AS [NATIONNALITE]
					, REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(MAX),[Livres].[mResume]),'';'','',''), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [RESUME]
					, Null AS [CRITIQUE]
					, [Publics].[sNom] AS [CIBLE]
					, (CASE WHEN [Livres].[iAnnee]>0 THEN [Livres].[iAnnee] ELSE Null END) AS [DATE_DE_SORTIE]
					, Null AS [DUREE]
					, Null AS [NOTE]
					, [Livres_Exemplaires].[sCodeBarre] AS [LIBELLE_EXEMPLAIRE] 
					, [Livres_Exemplaires].[sCodeBarre] AS [REF_EXEMPLAIRE]
					, [Livres_Exemplaires].[sCodeBarre] AS [CODE_BARRE]
					, [Livres].[sCote] AS [COTE]
					, Null AS [THEME]
					, [Livres].[sCollection] AS [COLLECTION]
					, Null AS [NUMEROTATION]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Livres_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=10 THEN [Livres_Supports].[sISBN] ELSE Null END) AS [ISBN10]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Livres_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=13 THEN [Livres_Supports].[sISBN] ELSE Null END) AS [ISBN13]
					, Null AS [DATE_EDITION]
					, [Medias].[iType] AS [TYPE_MEDIA]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Livres_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Livres_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) END) AS [EST_UNE_NOUVEAUTE]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Livres_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(d,[Sites].[iNouv],[Livres_Supports].[dDebCirculation])) ELSE Null END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Livres_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(M,[Sites].[iNouv],[Livres_Supports].[dDebCirculation])) ELSE Null END) END) AS [DATE_FIN_NOUVEAUTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Livres_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Livres].[iID] AND [Commandes].[sSupportID]=[Livres_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN 0 ELSE 1 END) AS [EN_FILE_D_ATTENTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Livres_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Livres].[iID] AND [Commandes].[sSupportID]=[Livres_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN DATEDIFF(d,GETDATE(),DATEADD(d,[Medias_Supports].[iDureeEmprunt],(SELECT TOP 1 dDebEmprunt FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Livres_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Livres].[iID] AND [Commandes].[sSupportID]=[Livres_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0))) ELSE [Medias_Supports].[iDureeEmprunt] END) AS [NOMBRE_DE_JOUR]
					, (CASE WHEN ([Livres_Supports].[vPrixValeur] * [Livres_Supports].[iQteTot])>0 THEN ([Livres_Supports].[vPrixValeur] * [Livres_Supports].[iQteTot]) ELSE Null END) AS [PRIX_ACHAT]
					, (CASE WHEN ([Livres_Supports].[vPrixValeur] * [Livres_Supports].[iQteTot])>0 THEN [Livres_Supports].[dCrea] ELSE Null END) AS [DATE_ACHAT]
					, Null AS [LIEU_ACHAT]'
		SET @sSQL_FROM_3 = N' FROM [Livres]
					LEFT JOIN [Livres_Exemplaires] ON [Livres].[iID]=[Livres_Exemplaires].[iLivresID]
					LEFT JOIN [Sites] ON [Livres_Exemplaires].[iSiteID]=[Sites].[iID]
					LEFT JOIN [Livres_Supports] ON [Livres_Exemplaires].[iLivresSupportID]=[Livres_Supports].[iID]
					LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
					ON [Livres_Supports].[sSupportID]=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=[Livres_Exemplaires].[iSiteID]
					LEFT JOIN [Personnalites] ON [Livres].[iPersoID]=[Personnalites].[iID]
					LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
					LEFT JOIN [Livres_Maisons] ON [Livres].[iID]=[Livres_Maisons].[iLivresID]
					LEFT JOIN [Maisons] ON [Livres_Maisons].[iMaisonID]=[Maisons].[iID]
					LEFT JOIN [Genres] ON [Livres].[iGenreID]=[Genres].[iID]
					LEFT JOIN [Publics] ON [Livres].[iPublicID]=[Publics].[iID]
					INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
					UNION'
		SET @sSQL_SELECT_4 = N' SELECT 
					[Perio].[iID]
					, ''Périodiques'' AS [LIBELLE_FAMILLE]
					, REPLACE(REPLACE(REPLACE((LTRIM(RTRIM(([Perio].[sArticle] + LTRIM(RTRIM([Perio].[sTitre])))))), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [LIBELLE_MEDIA]
					, Null AS [DESCRIPTION_MEDIA]
					, ([Personnalites].[sNom] + (CASE WHEN ISNULL([Personnalites].[sPrenom],'''') = '''' THEN '''' ELSE '', '' + [Personnalites].[sPrenom] END)) AS [AUTEUR]
					, Null AS [EDITEUR]
					, [Genres].[sNom] AS [GENRE]
					, [Nationalites].[sNom] AS [NATIONNALITE]
					, Null AS [RESUME]
					, Null AS [CRITIQUE]
					, [Publics].[sNom] AS [CIBLE]
					, [Perio_Numeros].[dParution] AS [DATE_DE_SORTIE]
					, Null AS [DUREE]
					, Null AS [NOTE]
					, [Perio_Exemplaires].[sCodeBarre] AS [LIBELLE_EXEMPLAIRE] 
					, [Perio_Exemplaires].[sCodeBarre] AS [REF_EXEMPLAIRE]
					, [Perio_Exemplaires].[sCodeBarre] AS [CODE_BARRE]
					, [Perio].[sCote] AS [COTE]
					, Null AS [THEME]
					, Null AS [COLLECTION]
					, Null AS [NUMEROTATION]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Perio_Numeros].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=10 THEN [Perio_Numeros].[sISBN] ELSE Null END) AS [ISBN10]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Perio_Numeros].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=13 THEN [Perio_Numeros].[sISBN] ELSE Null END) AS [ISBN13]
					, Null AS [DATE_EDITION]
					, [Medias].[iType] AS [TYPE_MEDIA]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Perio_Numeros].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Perio_Numeros].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) END) AS [EST_UNE_NOUVEAUTE]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Perio_Numeros].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(d,[Sites].[iNouv],[Perio_Numeros].[dDebCirculation])) ELSE Null END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Perio_Numeros].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(M,[Sites].[iNouv],[Perio_Numeros].[dDebCirculation])) ELSE Null END) END) AS [DATE_FIN_NOUVEAUTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Perio_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Perio].[iID] AND [Commandes].[sNumero]=[Perio_Numeros].[sNumero] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN 0 ELSE 1 END) AS [EN_FILE_D_ATTENTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Perio_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Perio].[iID] AND [Commandes].[sNumero]=[Perio_Numeros].[sNumero] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN DATEDIFF(d,GETDATE(),DATEADD(d,[Medias_Supports].[iDureeEmprunt],(SELECT TOP 1 dDebEmprunt FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Perio_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Perio].[iID] AND [Commandes].[sNumero]=[Perio_Numeros].[sNumero] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0))) ELSE [Medias_Supports].[iDureeEmprunt] END) AS [NOMBRE_DE_JOUR]
					, (CASE WHEN ([Perio_Numeros].[vPrixValeur] * [Perio_Numeros].[iQteTot])>0 THEN ([Perio_Numeros].[vPrixValeur] * [Perio_Numeros].[iQteTot]) ELSE Null END) AS [PRIX_ACHAT]
					, (CASE WHEN ([Perio_Numeros].[vPrixValeur] * [Perio_Numeros].[iQteTot])>0 THEN [Perio_Numeros].[dCrea] ELSE Null END) AS [DATE_ACHAT]
					, Null AS [LIEU_ACHAT]'
		SET @sSQL_FROM_4 = N' FROM [Perio]
					LEFT JOIN [Perio_Exemplaires] ON [Perio].[iID]=[Perio_Exemplaires].[iPerioID]
					LEFT JOIN [Perio_Numeros] ON [Perio_Exemplaires].[iNumeroID]=[Perio_Numeros].[iID]
					LEFT JOIN [Douchette] ON [Perio_Exemplaires].[sCodeBarre]=[Douchette].[sCodeBarre]
					LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
					ON [Douchette].[iTypeID]=[Medias].[iID]
					LEFT JOIN [Sites] ON [Perio_Exemplaires].[iSiteID]=[Sites].[iID]
					LEFT JOIN [Genres] ON [Perio].[iGenreID]=[Genres].[iID]
					LEFT JOIN [Publics] ON [Perio].[iPublicID]=[Publics].[iID]
					LEFT JOIN [Personnalites] ON [Perio].[iPersoID]=[Personnalites].[iID]
					LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
					INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]
					UNION'
		SET @sSQL_SELECT_5 = N' SELECT 
					[Video].[iID]
					, [Medias].[sNom] AS [LIBELLE_FAMILLE]
					, REPLACE(REPLACE(REPLACE((LTRIM(RTRIM(([Video].[sArticle] + LTRIM(RTRIM([Video].[sTitre])))))), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [LIBELLE_MEDIA]
					, [Medias_Supports].[sNom] AS [DESCRIPTION_MEDIA]
					, ([Personnalites].[sNom] + (CASE WHEN ISNULL([Personnalites].[sPrenom],'''') = '''' THEN '''' ELSE '', '' + [Personnalites].[sPrenom] END)) AS [AUTEUR]
					, [Maisons].[sNom] AS [EDITEUR]
					, [Genres].[sNom] AS [GENRE]
					, [Nationalites].[sNom] AS [NATIONNALITE]
					, REPLACE(REPLACE(REPLACE(REPLACE(CONVERT(VARCHAR(MAX),[Video].[mDescription]),'';'','',''), CHAR(13) + CHAR(10),'' ''), CHAR(10),'' ''), CHAR(13),'' '') AS [RESUME]
					, Null AS [CRITIQUE]
					, [Publics].[sNom] AS [CIBLE]
					, (CASE WHEN [Video].[iAnnee]>0 THEN [Video].[iAnnee] ELSE Null END) AS [DATE_DE_SORTIE]
					, Null AS [DUREE]
					, Null AS [NOTE]
					, [Video_Exemplaires].[sCodeBarre] AS [LIBELLE_EXEMPLAIRE] 
					, [Video_Exemplaires].[sCodeBarre] AS [REF_EXEMPLAIRE]
					, [Video_Exemplaires].[sCodeBarre] AS [CODE_BARRE]
					, [Video].[sCote] AS [COTE]
					, Null AS [THEME]
					, Null AS [COLLECTION]
					, Null AS [NUMEROTATION]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Video_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=10 THEN [Video_Supports].[sISBN] ELSE Null END) AS [ISBN10]
					, (CASE WHEN LTRIM(RTRIM(LEN(REPLACE(REPLACE(REPLACE([Video_Supports].[sISBN],'' '',''''),''.'',''''),''-'',''''))))=13 THEN [Video_Supports].[sISBN] ELSE Null END) AS [ISBN13]
					, Null AS [DATE_EDITION]
					, [Medias].[iType] AS [TYPE_MEDIA]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Video_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Video_Supports].[dDebCirculation])),GETDATE())<0 THEN 1 ELSE 0 END) END) AS [EST_UNE_NOUVEAUTE]
					, (CASE WHEN UPPER([Sites].[sNouv])=''J'' THEN (CASE WHEN DATEDIFF(d,(DATEADD(d,[Sites].[iNouv],[Video_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(d,[Sites].[iNouv],[Video_Supports].[dDebCirculation])) ELSE Null END) ELSE (CASE WHEN DATEDIFF(d,(DATEADD(M,[Sites].[iNouv],[Video_Supports].[dDebCirculation])),GETDATE())<0 THEN (DATEADD(M,[Sites].[iNouv],[Video_Supports].[dDebCirculation])) ELSE Null END) END) AS [DATE_FIN_NOUVEAUTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Video_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Video].[iID] AND [Commandes].[sSupportID]=[Video_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN 0 ELSE 1 END) AS [EN_FILE_D_ATTENTE]
					, (CASE WHEN (SELECT COUNT(*) FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Video_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Video].[iID] AND [Commandes].[sSupportID]=[Video_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0)>0 THEN DATEDIFF(d,GETDATE(),DATEADD(d,[Medias_Supports].[iDureeEmprunt],(SELECT TOP 1 dDebEmprunt FROM [Commandes] WHERE [Commandes].[iExemplaireID]=[Video_Exemplaires].[iID] AND [Commandes].[iMediaID]=[Video].[iID] AND [Commandes].[sSupportID]=[Video_Supports].[sSupportID] AND DATEDIFF(d,ISNULL([Commandes].[dRestitue],DATEADD(d,1,GETDATE())),GETDATE())<0))) ELSE [Medias_Supports].[iDureeEmprunt] END) AS [NOMBRE_DE_JOUR]
					, (CASE WHEN ([Video_Supports].[vPrixValeur] * [Video_Supports].[iQteTot])>0 THEN ([Video_Supports].[vPrixValeur] * [Video_Supports].[iQteTot]) ELSE Null END) AS [PRIX_ACHAT]
					, (CASE WHEN ([Video_Supports].[vPrixValeur] * [Video_Supports].[iQteTot])>0 THEN [Video_Supports].[dCrea] ELSE Null END) AS [DATE_ACHAT]
					, Null AS [LIEU_ACHAT]'
		SET @sSQL_FROM_5 = N' FROM [Video]
					LEFT JOIN [Video_Exemplaires] ON [Video].[iID]=[Video_Exemplaires].[iVideoID]
					LEFT JOIN [Sites] ON [Video_Exemplaires].[iSiteID]=[Sites].[iID]
					LEFT JOIN [Video_Supports] ON [Video_Exemplaires].[iVideoSupportID]=[Video_Supports].[iID]
					LEFT JOIN ([Medias] LEFT JOIN [Medias_Supports] ON [Medias_Supports].[iMediaID]=[Medias].[iID])
					ON ISNULL([Video_Supports].[sSupportID],[Medias_Supports].[sSupportID])=[Medias_Supports].[sSupportID] AND [Medias_Supports].[iSiteID]=ISNULL([Video_Exemplaires].[iSiteID],[Medias_Supports].[iSiteID])
					LEFT JOIN [Personnalites] ON [Video].[iPersoID]=[Personnalites].[iID]
					LEFT JOIN [Nationalites] ON [Personnalites].[iNationaliteID]=[Nationalites].[iID]
					LEFT JOIN [Video_Maisons] ON [Video].[iID]=[Video_Maisons].[iVideoID]
					LEFT JOIN [Maisons] ON [Video_Maisons].[iMaisonID]=[Maisons].[iID]
					LEFT JOIN [Genres] ON [Video].[iGenreID]=[Genres].[iID]
					LEFT JOIN [Publics] ON [Video].[iPublicID]=[Publics].[iID]
					INNER JOIN [Stocks] ON [Sites].[iID]=[Stocks].[iSiteID]) AS dtbExport
					) AS dtbExport2
					WHERE ([dtbExport2].[RowNum]>CONVERT(INT,' + CONVERT(VARCHAR,(@i*@iLignesMAX)) + '))
					AND  ([dtbExport2].[RowNum]<=CONVERT(INT,' + CONVERT(VARCHAR,((@i+1)*@iLignesMAX)) + '))
					ORDER BY [LIBELLE_MEDIA]'
			--PRINT N'' + CHAR(13) + CHAR(10)
			--PRINT @sSQL_SELECT_1
			--PRINT @sSQL_FROM_1
			--PRINT @sSQL_SELECT_2
			--PRINT @sSQL_FROM_2
			--PRINT @sSQL_SELECT_3
			--PRINT @sSQL_FROM_3
			--PRINT @sSQL_SELECT_4
			--PRINT @sSQL_FROM_4
			--PRINT @sSQL_SELECT_5
			--PRINT @sSQL_FROM_5
			--PRINT N'' + CHAR(13) + CHAR(10)
			PRINT N'' + @sSQL_SELECT_1 + @sSQL_FROM_1 + @sSQL_SELECT_2 + @sSQL_FROM_2 + @sSQL_SELECT_3 + @sSQL_FROM_3 + @sSQL_SELECT_4 + @sSQL_FROM_4 + @sSQL_SELECT_5 + @sSQL_FROM_5
			PRINT N'' + CHAR(13) + CHAR(10)

			EXEC (@sSQL_SELECT_1 + @sSQL_FROM_1 + @sSQL_SELECT_2 + @sSQL_FROM_2 + @sSQL_SELECT_3 + @sSQL_FROM_3 + @sSQL_SELECT_4 + @sSQL_FROM_4 + @sSQL_SELECT_5 + @sSQL_FROM_5) 
			SET @i = @i + 1
			
			PRINT '-----------------------------------------------------------------------------------'
			PRINT N'' + CHAR(13) + CHAR(10)
			PRINT N'' + CHAR(13) + CHAR(10)
	END		
END
